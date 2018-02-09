using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace SearchSelectResultBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync($"Enter a search term");

            context.Wait(MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            //get search term. This might be an entity in LUIS bots using something lik
            //var entity = result.Entities.FirstOrDefault(e => e.Type == "SeachTerm");
            //var searchTerm = entity == null ? "" : entity.Entity;
            var searchTerm = activity.Text;

            //forward context to search results dialog. Pass search term to new dialog
            await context.Forward(new SearchResultsDialog(searchTerm), this.ResumeAfterSearchResultsDialog, result, CancellationToken.None);

            context.Wait(MessageReceivedAsync);
        }

        private async Task ResumeAfterSearchResultsDialog(IDialogContext context, IAwaitable<object> result)
        {
            var chosenResultActivity = await result as IMessageActivity;

            var chosenResult = chosenResultActivity.Text;

            await context.PostAsync($"Chosen result {chosenResult}");

            context.Done("");

        }
    }
}