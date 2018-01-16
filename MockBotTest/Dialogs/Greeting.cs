using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;

namespace MockBotTest.Dialogs
{
    [Serializable]
    public class Greeting : IDialog

    {
        public async Task StartAsync(IDialogContext context)
        {

            await context.PostAsync("Greetings are in order!!");
            await Respond(context);

            context.Wait(MessageRecievedAsync);

        }

        public virtual async Task Respond (IDialogContext context)
        {
            var userName = String.Empty;
            context.UserData.TryGetValue<string>("Name", out userName);
            if (string.IsNullOrEmpty(userName))
            {
                await context.PostAsync("Give me your name!!");
                context.UserData.SetValue<bool>("GetName", true);
            }
            else
            {
                await context.PostAsync(String.Format("Hi {0}. I'm glad to have you here. Any talk?", userName));
            }
        }

        public virtual async Task MessageRecievedAsync(IDialogContext context, IAwaitable<Microsoft.Bot.Connector.IMessageActivity> argument)
        {
            var message = await argument;
            var userName = String.Empty;
            var GetName = false;
            context.UserData.TryGetValue<bool>("GetName", out GetName);

            if(GetName)
            {
                userName = message.Text;
                context.UserData.SetValue<string>("Name", userName);
                context.UserData.SetValue<bool>("GetName", false);
            }

            await Respond(context);
            context.Done(message);

            //context.Wait(MessageRecievedAsync);
            //throw new NotImplementedException();
        }
    }
}