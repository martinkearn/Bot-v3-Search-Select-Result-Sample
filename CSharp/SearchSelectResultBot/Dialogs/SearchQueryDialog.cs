using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace SearchSelectResultBot.Dialogs
{
    [Serializable]
    public class SearchQueryDialog : IDialog<object>
    {
        protected int count = 1;

        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Enter a search term");
            context.Wait(MessageReceivedAsync);
        }

        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var message = await argument;
            var searchTerm = message.Text;
            await context.Forward(new SearchResultsDialog(searchTerm), this.ResumeAfterSearchResultsDialog, message, CancellationToken.None);
        }

        private async Task ResumeAfterSearchResultsDialog(IDialogContext context, IAwaitable<object> result)
        {
            var message = await result as IMessageActivity;
            var chosenResult = message.Text;
            await context.PostAsync($"You chose {chosenResult}");
            context.Done("");
        }
    }
}