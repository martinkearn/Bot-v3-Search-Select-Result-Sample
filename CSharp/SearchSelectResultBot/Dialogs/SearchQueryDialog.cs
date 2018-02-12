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
            //Prompt the user to enter a search term and handle the response in MessageReceivedAsync
            await context.PostAsync("Enter a search term");
            context.Wait(MessageReceivedAsync);
        }

        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            //Grab the search term that the user entered
            var message = await argument;
            var searchTerm = message.Text;

            //Forward the context to the SearchResultsDialog and pass in the searchTerm. Process the result from SearchResultsDialog in ResumeAfterSearchResultsDialog
            //You could also pass searchTerm data between dialogs using Bot State (required for complex types) which can be set with context.ConversationData.SetValue("SearchTerm", searchTerm);
            await context.Forward(new SearchResultsDialog(searchTerm), this.ResumeAfterSearchResultsDialog, message, CancellationToken.None);
        }

        private async Task ResumeAfterSearchResultsDialog(IDialogContext context, IAwaitable<object> result)
        {
            //Grab the resut sentback from SearchResultsDialog when is was closed with context.Done
            var message = await result as Activity;
            var chosenResult = message.Text;

            //Tell the user what they selected. More typically you'd do something with the result here
            await context.PostAsync($"You chose {chosenResult}");

            //Close context passing it back up to RootDialog
            context.Done("");
        }
    }
}