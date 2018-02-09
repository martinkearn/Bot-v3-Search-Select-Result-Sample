using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SearchSelectResultBot.Dialogs
{
    [Serializable]
    public class SearchResultsDialog : IDialog<object>
    {
        private readonly string _searchQuery;

        public SearchResultsDialog(string searchQuery)
        {
            _searchQuery = searchQuery;
        }

        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync($"You searched for {_searchQuery} and I found several matches");

            context.Wait(this.MessageReceivedAsync);
        }

        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            //Generate some results. This could be an API call for some sort
            var results = new List<string>();
            var rdnm = new Random(_searchQuery.GetHashCode());
            results.Add(rdnm.Next().ToString());
            results.Add(rdnm.Next().ToString());
            results.Add(rdnm.Next().ToString());
            results.Add(rdnm.Next().ToString());
            results.Add(rdnm.Next().ToString());

            //create card
            var msg = context.MakeMessage();
            msg.Text = "Which one did you mean?";
            msg.Attachments.Add(new HeroCard()
            {
                Title = "Results",
                Buttons = results.Select(x => new CardAction()
                {
                    Title = $"{x}",
                    Type = ActionTypes.ImBack,
                    Value = x
                }).ToList()
            }.ToAttachment());

            await context.PostAsync(msg);

            context.Wait(ResponseReceivedAsync);
        }

        public virtual async Task ResponseReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;

            context.Done(message);
        }
    }
}