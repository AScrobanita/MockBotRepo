using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using MockBotTest.Models;

namespace MockBotTest.Dialogs
{
    public class ReleaseBot
    {
        public static readonly IDialog<string> dialog = Chain.PostToChain()
            .Select(msg => msg.Text)
            .Switch(new RegexCase<IDialog<string>>(new .Regex("Hi", RegexOptions.IgnoreCase), (context, text) => {
                return Chain.ContinueWith(new Greeting(), AfterGreedingContinuation);
            }
            ),
            new DefaultCase<string, IDialog<string>>((context, text) =>
            {
                return Chain.ContinueWith(FormDialog.FromForm(ReleaseForm.BuildForm, FormOptions.PromptInStart), AfterGreedingContinuation);
            }))
            .Unwrap()
            .PostToUser();
private async static Task<IDialog<string>> AfterGreedingContinuation(IBotContext context, IAwaitable<object> item)
        {
            var token = await item;
            var name = "User";
            context.UserData.TryGetValue<string>("name", out name);
            return Chain.Return($"Thanks for using release bot: {name}");
                        // throw new NotImplementedException();
        }
    }
}