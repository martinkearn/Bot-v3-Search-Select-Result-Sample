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
            //Grab the search term sent with the constructor and store it in a private variable for use throughout the dialog
            _searchQuery = searchQuery;

            //You could have also passed data via Bot State (required for complex types). See comments in StartAsync
        }

        public async Task StartAsync(IDialogContext context)
        {
            //If you had used Bot State to pass data between dialogs, you'd read it here using something like
            //var searchTerm = new String();
            //context.ConversationData.TryGetValue("SearchTerm", out searchTerm);

            context.Wait(this.MessageReceivedAsync);
        }

        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            //Generate some results. This could be an API call for some sort
            var searchResults = new List<string>();
            searchResults.Add($"{_searchQuery} Result A");
            searchResults.Add($"{_searchQuery} Result B");
            searchResults.Add($"{_searchQuery} Result C");
            searchResults.Add($"{_searchQuery} Result D");

            //Create and show card of buttons representing the search results
            var msg = context.MakeMessage();
            msg.Text = $"You searched for {_searchQuery} and I found several matches. Which one did you mean?";
            msg.Attachments.Add(new HeroCard()
            {
                Title = "Results",
                Buttons = searchResults.Select(x => new CardAction()
                {
                    Title = $"{x}",
                    Type = ActionTypes.ImBack,
                    Value = x
                }).ToList()
            }.ToAttachment());
            await context.PostAsync(msg);

            //await user's response and process it in ResponseReceivedAsync
            context.Wait(ResponseReceivedAsync);
        }

        public virtual async Task ResponseReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            //Grab the message the user sent by clicking one of the button choices. Typically you'd also handle scenarios where the user replies with text not contained in the button list here
            var message = await result;

            //Close context passing it back up to SearchQueryDialog
            context.Done(message);
        }
    }
}