using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace AuraRT.Common
{
    public class IncrementalLoadingCollection<T, I> : ObservableCollection<I>,
        ISupportIncrementalLoading where T : IIncrementalSource<I>, new()
    {
        public event EventHandler<IncrementalLoadingStartedEventArgs> LoadingStarted;
        public event EventHandler<IncrementalLoadingCompletedEventArgs> LoadingCompleted;

        private T source;
        private int itemsPerPage;
        private bool hasMoreItems;
        private int currentPage;
        private string param;

        public IncrementalLoadingCollection(int itemsPerPage = 20, string param=null)
        {
            this.source = new T();
            this.itemsPerPage = itemsPerPage;
            this.hasMoreItems = true;
            this.param = param;
        }

        public bool HasMoreItems
        {
            get { return hasMoreItems; }
        }

        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            var dispatcher = Window.Current.Dispatcher;

            return Task.Run<LoadMoreItemsResult>(
                async () =>
                {
                    uint resultCount = 0;
                    var result = await source.GetPagedItems(currentPage++, itemsPerPage, param);

                    if(result == null || result.Count() == 0)
                    {
                        hasMoreItems = false;
                    }
                    else
                    {

                        if(LoadingStarted != null)
                        {
                            await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                            {
                                LoadingStarted(this, new IncrementalLoadingStartedEventArgs(count));
                            });
                        }

                        resultCount = (uint)result.Count();
                        await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                        {
                            foreach(I item in result)
                                this.Add(item);
                        });
                    }

                    if(LoadingCompleted != null)
                    {
                        await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                        {
                            LoadingCompleted(this, new IncrementalLoadingCompletedEventArgs(resultCount));
                        });
                    }

                    return new LoadMoreItemsResult() { Count = resultCount };

                }).AsAsyncOperation<LoadMoreItemsResult>();
        }
    }

    public class IncrementalLoadingStartedEventArgs
    {
        private uint count;

        public IncrementalLoadingStartedEventArgs(uint count)
        {
            this.count = count;
        }
    }

    public class IncrementalLoadingCompletedEventArgs
    {
        public uint ItemsCount { get; set; }

        public IncrementalLoadingCompletedEventArgs(uint itemsCount)
        {
            ItemsCount = itemsCount;
        }
    }
}
