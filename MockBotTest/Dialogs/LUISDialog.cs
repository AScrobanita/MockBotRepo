using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Luis;
using MockBotTest.Models;

namespace MockBotTest.Dialogs
{
    [LuisModel("f118c92 - 8bda - 40cb - 8f8b - bd340f1bc61f", "239df4d63e4c4428bc7afb4f63002cfb")]
    [Serializable]
    public class LUISDialog: LuisDialog<ReleaseForm>
    {

        private readonly BuildFormDelegate<ReleaseForm> Release;

        public LUISDialog(BuildFormDelegate<ReleaseForm> ReleaseAction)
        {
            this.Release = ReleaseAction;
        }


        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisServiceResult result)
        {
            await context.PostAsync("Unfortunately, I don't know or have the ability to understand this command");
            context.Wait(MessageReceived);
        }

        [LuisIntent("Greeting")]
        public async Task Greeting(IDialogContext context, LuisServiceResult result)
        {
            context.Call(new Greeting(), Callback);
        }

        private async Task Callback(IDialogContext context, IAwaitable<object> result)
        {
            context.Wait(MessageReceived);

            // throw new NotImplementedException();
        }


        [LuisIntent("Release")]
        public async Task ReleaseAction(IDialogContext context, LuisServiceResult result)
        {
            var releaseForm = new FormDialog<ReleaseForm>(new ReleaseForm(), this.Release, FormOptions.PromptInStart);
            context.Call<ReleaseForm>(releaseForm, Callback);
        }
    }
}