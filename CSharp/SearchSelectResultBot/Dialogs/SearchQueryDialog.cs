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

            await context.PostAsync($"You searched for {searchTerm}");

            context.Done("");
        }

        //public async Task AfterResetAsync(IDialogContext context, IAwaitable<bool> argument)
        //{
        //    var confirm = await argument;
        //    if (confirm)
        //    {
        //        this.count = 1;
        //        await context.PostAsync("Reset count.");
        //    }
        //    else
        //    {
        //        await context.PostAsync("Did not reset count.");
        //    }
        //    context.Wait(MessageReceivedAsync);
        //}
    }
}