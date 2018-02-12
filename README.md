# Bot Search and Select Result Sample
Having a users select from a list of items is a relatively common scenario for bots. However, the implementation of this scenario requires a relatively complex chain of Dialogs with data being passed between them both in terms of the user's initial query and the chosen result coming back down the chain.

This repository contains a simple example of such an implementation. It uses a simple `IDialog` which initiates the process in response to any message but this could easily be substituted with a `LuisDialog` fr handling different intents.

The sample is a simple Bot Application created from the [BotBuilder template](https://docs.microsoft.com/en-us/bot-framework/dotnet/bot-builder-dotnet-quickstart). The interesting bits start at [RootDialog.cs](https://github.com/martinkearn/Bot-Search-Select-Result-Sample/blob/master/CSharp/SearchSelectResultBot/Dialogs/RootDialog.cs).

This is the basic flow this sample achieves.

![Bot search results conversation](https://github.com/martinkearn/Bot-Search-Select-Result-Sample/raw/master/SearchResultFlow.JPG)
