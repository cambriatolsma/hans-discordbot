using Discord;
using Discord.Commands;
using System;
using System.Diagnostics; 
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Discord.WebSocket;

namespace Hans.Modules
{
    public class Test : ModuleBase<SocketCommandContext>
    {
        Random rand;

        string[] Responses;

        [Command("!")]
        public async Task general()
        {
            await Context.Channel.SendMessageAsync("🔥 ***F L A M M E N  W E R F E R*** 🔥");
        }
        
        [Command("was ist dein Typ?")]
        public async Task another()
        {
            await Context.Channel.SendFileAsync("memes/hasselhoff.jpg");
            await Context.Channel.SendMessageAsync("*h a s s e l h o f f*");
        }

        [Command("wie geht's?")]
        public async Task poorHans()//SocketGuildUser userToKick)
        {
            Debug.WriteLine("hello?");
            rand = new Random();

            Responses = new string[]
            {
                "AAAAAAAAAAAAA",
                "Warum bin ich hier? Nur um zu leiden?",
                "Ich liebe dich ♥",
                "Oh you know, always on the verge of death. Typical day on the Eastern front.",
                "Do you really care?",
                "Wie Sie, nur schlechter.",
                "My commander says everything is fine. Just. Fine.",
                "Can't complain. I'll get sent to the work camps if I do.",
                "Ja. 👍",
                "Good, as long as you aren't Russian.",
                "Why do you ask? Are you a doctor? Can I go home?",
                "***ICH BRAUCHE MEHR FEUER!***"
            };

            int randomResponse = rand.Next(Responses.Length);
            string messageToPost = Responses[randomResponse];

            if(messageToPost.CompareTo(Responses[0]) == 0)
            {
                await Context.Channel.SendMessageAsync(messageToPost);

                var name = Context.Message.Author.ToString();
                var delim = name.IndexOf("#");
                var discrim = name.Substring(delim + 1); //Context.Message.Author.Discriminator;
                var username = name.Substring(0, delim - 1); //Context.Message.Author.Username;

                SocketGuildUser hi = (Context.Client.GetUser(username, discrim) as SocketGuildUser);
                //var userToKick = (Context.Client.GetUser(username, discrim) as SocketGuildUser); //Context.Message.Author.Username; 
                //await Context.Channel.SendMessageAsync(userToKick.Mention);
                //await userToKick.KickAsync();//"You have unfortunately caused Hans to snap. Aufweidersehen!");  
            }

            else
            {
                await Context.Channel.SendMessageAsync(messageToPost);
            }
        }

        [Command("no u")]
        public async Task nou()
        {
            await Context.Channel.SendFileAsync("memes/nonou.png");
        }

        [Command("hilf!")]
        public async Task blah([Remainder]string userMsg)
        {
            userMsg = userMsg.ToLower();

            if (userMsg.Contains("russia") || userMsg.Contains("russian") || userMsg.Contains("russians"))
            {
                await Context.Channel.SendMessageAsync("Rette mich, Heiliger Rommel!");
            }
        }

        [Command("r6")]
        public async Task statsGrab([Remainder]string userMsg)
        {
            Debug.WriteLine("This is a test");
            var input = userMsg.Split(' ');
            string platform = input[0];
            string userName = input[1];

            string url = "https://r6.tracker.network/profile/" + platform + "/" + userName;
            var Webget = new HtmlWeb();
            var doc = Webget.Load(url);
            
            var ranking = doc.DocumentNode.SelectNodes("//*[@id='profile']/div[3]/div[2]/div[1]/div[1]/div/div[2]/div/div[2]");
            await Context.Channel.SendMessageAsync(ranking[0].InnerText); 
        }
    }
}
