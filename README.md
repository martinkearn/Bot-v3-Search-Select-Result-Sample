# Bot Search and Select Result Sample
Having a users select from a list of results is a relatively common scenario for bots. This could be simple search results, disambiguating entities or many other scenarios.

The implementation of this scenario requires a relatively complex chain of Dialogs with data being passed between them both in terms of the user's initial query and the chosen result coming back down the chain.

This repository contains a simple example of such an implementation. It uses a simple `IDialog` which initiates the process in response to any message but this could easily be substituted with a `LuisDialog` fr handling different intents.

This is the basic flow this sample achieves.

![Bot search results conversation](https://github.com/martinkearn/Bot-Search-Select-Result-Sample/raw/master/SearchResultFlow.JPG)
