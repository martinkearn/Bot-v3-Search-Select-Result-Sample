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
            context.Wait(this.MessageReceivedAsync);
        }

        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            //Call SearchQueryDialog regardless of what the user says
            //More typically this function would be a LUIS intent handler
            context.Call(new SearchQueryDialog(), ResumeAfterSearchQueryDialog);
        }

        private async Task ResumeAfterSearchQueryDialog(IDialogContext context, IAwaitable<object> result)
        {
            //just close the context and do nothing with the result
            context.Done("");
        }

    }
}