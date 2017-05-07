using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Threading;
using TeleSharp.Entities;
using TeleSharp.Entities.Inline;
using TeleSharp.Entities.SendEntities;

namespace SampleBot
{
    internal class Program
    {
        public static TeleSharp.TeleSharp Bot;

        private static void Main(string[] args)
        {
            Bot = new TeleSharp.TeleSharp("314017437:AAGghBtoUHdxl9nKeX85EV3Rtx-zRM0dT8E");
            Bot.SendMessage(new SendMessageParams
            {
                ChatId = "39699831",
                Text = "Test msg !",
                InlineKeyboard = new InlineKeyboardMarkup
                {
                    InlineKeyboard = new List<List<InlineKeyboardButton>>
                    {
                        new List<InlineKeyboardButton>
                        {
                             new InlineKeyboardButton {Text="CallbackData",CallbackData="Ok",SwitchInlineQuery=string.Empty,SwitchInlineQueryCurrentChat=string.Empty,Url=string.Empty },
                        },
                        new List<InlineKeyboardButton>
                        {
                             new InlineKeyboardButton {Text="SwitchInlineQueryCurrentChat",CallbackData=string.Empty,SwitchInlineQuery=string.Empty,SwitchInlineQueryCurrentChat="OK",Url=string.Empty },
                        },
                         new List<InlineKeyboardButton>
                        {
                             new InlineKeyboardButton {Text="Url",CallbackData=string.Empty,SwitchInlineQuery=string.Empty,SwitchInlineQueryCurrentChat=string.Empty,Url="http://dualp.ir" },
                        },
                        new List<InlineKeyboardButton>
                        {
                              new InlineKeyboardButton {Text="SwitchInlineQuery",SwitchInlineQuery="سلام",Url=string.Empty,CallbackData=string.Empty,SwitchInlineQueryCurrentChat=string.Empty }
                        }
                    }
                }
            });
            Bot.OnMessage += OnMessage;
            Bot.OnInlineQuery += OnInlineQuery;
            Bot.OnCallbackQuery += Bot_OnCallbackQuery;
            
            Console.WriteLine($"Hi, My Name is : {Bot.Me.Username}");

            Console.ReadLine();
        }

        private static void Bot_OnCallbackQuery(CallbackQuery CallbackQuery)
        {
            Bot.AnswerCallbackQuery(CallbackQuery, "Hello");
            Bot.EditMessageText(new SendMessageParams
            {
                ChatId = CallbackQuery.Message.Chat.Id.ToString(),
                MessageId = CallbackQuery.Message.MessageId.ToString(),
                Text=CallbackQuery.Message.Text,
                InlineKeyboard = new InlineKeyboardMarkup
                {
                    InlineKeyboard = new List<List<InlineKeyboardButton>>
                     {
                         new List<InlineKeyboardButton>
                         {
                             new InlineKeyboardButton {Text="Test",CallbackData="OK",SwitchInlineQuery=string.Empty,SwitchInlineQueryCurrentChat=string.Empty,Url=string.Empty }
                         }
                     }
                }
            });
        }

        private static void OnInlineQuery(InlineQuery inlinequery)
        {
            Bot.AnswerInlineQuery(new AnswerInlineQuery
            {
                InlineQueryId = inlinequery.Id,
                Results = new List<InlineQueryResult>
                {
                    new InlineQueryResultArticle
                    {
                        Id = inlinequery.Query,
                        Title = DateTime.Now.ToLongDateString(),
                        MessageText = Guid.NewGuid().ToString(),
                        ParseMode = "",
                        Url = "",
                        DisableWebPagePreview = false,
                        Description = "",
                        HideUrl = false,
                        ThumbHeight = 0,
                        ThumbWidth = 0,
                        ThumbUrl = ""
                    }
                },
                IsPersonal = false,
                CacheTime = 300,
                NextOffset = "0"
            });
        }

        /// <summary>
        /// Read received messages of bot in infinity loop
        /// </summary>
        private static void OnMessage(Message message)
        {
            // Get mesage sender information
            MessageSender sender = (MessageSender)message.Chat ?? message.From;
            
            Console.WriteLine(message.Text ?? "");

            if(!string.IsNullOrEmpty(message.Text))
            message.Text = message.Text.Split('@')[0];

            // If user joined to bot say welcome
            if ((!string.IsNullOrEmpty(message.Text)) && (message.Text == "/benvenuto"))
            {
                string welcomeMessage =
                    $"Ciao mbare {message.From.Username}!{Environment.NewLine}Il mio nome è {Bot.Me.Username}{Environment.NewLine}Ti consiglio subito un sito : http://www.lupoporno.com";

                Bot.SendMessage(new SendMessageParams
                {
                    ChatId = sender.Id.ToString(),
                    Text = welcomeMessage
                });
                return;
            }

            string baseStoragePath = ConfigurationManager.AppSettings["Data"];

            // If any file exists in message download it
            DownloadFileFromMessage(message, baseStoragePath);
            // If Send Location or Contact
            GetLocationContactFromMessage(message, sender);

            if (string.IsNullOrEmpty(message.Text) || string.IsNullOrEmpty(baseStoragePath))
                return;

            try
            {
                string sampleData = Path.Combine(baseStoragePath, "SampleData");

                if (!string.IsNullOrEmpty(message.Text))
                    switch (message.Text.ToLower())
                    {
                        //case "time":
                        //    {
                        //        Bot.SendMessage(new SendMessageParams
                        //        {
                        //            ChatId = sender.Id.ToString(),
                        //            Text = DateTime.Now.ToLongDateString()
                        //        });
                        //        break;
                        //    }

                        //case "location":
                        //    {
                        //        Bot.SendLocation(sender, "50.69421", "3.17456");
                        //        break;
                        //    }

                        //case "sticker":
                        //    {
                        //        Bot.SendSticker(sender, System.IO.File.ReadAllBytes(Path.Combine(sampleData, "sticker.png")));
                        //        break;
                        //    }

                        case "/apriporta":
                            {
                                //WebClient client = new WebClient();

                                //client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

                                //Stream data = client.OpenRead("http://192.168.1.3/ws/home/open");
                                //StreamReader reader = new StreamReader(data);
                                //string s = reader.ReadToEnd();
                                //Console.WriteLine(s);
                                //data.Close();
                                //reader.Close();
                                //break;
                                Bot.SendMessage(new SendMessageParams
                                {
                                    ChatId = sender.Id.ToString(),
                                    Text = $"Non sei autorizzato!{Environment.NewLine}Il tuo smartphone si autodistruggerà tra..."
                                });
                                Thread.Sleep(2000);
                                for (int i = 3; i > 0; i--)
                                {
                                    Thread.Sleep(1000);
                                    Bot.SendMessage(new SendMessageParams
                                    {
                                        ChatId = sender.Id.ToString(),
                                        Text = i.ToString()
                                    });
                                }
                                Thread.Sleep(1000);                                
                                Bot.SendMessage(new SendMessageParams
                                {
                                    ChatId = sender.Id.ToString(),
                                    Text = "Boom!!!!"
                                });
                                break;
                            }

                        case "/semplice":
                            {
                                string photoFilePath = Path.Combine(sampleData, "mantra.jpg");

                                Bot.SetCurrentAction(sender, ChatAction.UploadPhoto);
                                Bot.SendPhoto(sender, System.IO.File.ReadAllBytes(photoFilePath),
                                    Path.GetFileName(photoFilePath), "OrangeBoss");
                                break;
                            }
                        case "/alessandro":
                            {
                                string photoFilePath = Path.Combine(sampleData, "mannaggia.jpg");

                                Bot.SetCurrentAction(sender, ChatAction.UploadPhoto);
                                Bot.SendPhoto(sender, System.IO.File.ReadAllBytes(photoFilePath),
                                    Path.GetFileName(photoFilePath));
                                break;
                            }
                        case "/ottimismo":
                            {
                                string audioFilePath = Path.Combine(sampleData, "ottimismo.mp3");

                                Bot.SetCurrentAction(sender, ChatAction.UploadAudio);
                                Bot.SendAudio(sender, System.IO.File.ReadAllBytes(audioFilePath),
                                    Path.GetFileName(audioFilePath));
                                break;
                            }

                        //case "video":
                        //    {
                        //        string videoFilePath = Path.Combine(sampleData, "video.mp4");

                        //        Bot.SetCurrentAction(sender, ChatAction.UploadVideo);
                        //        Bot.SendVideo(sender, System.IO.File.ReadAllBytes(videoFilePath),
                        //            Path.GetFileName(videoFilePath), "This is sample video");
                        //        break;
                        //    }

                        //case "audio":
                        //    {
                        //        string audioFilePath = Path.Combine(sampleData, "audio.mp3");

                        //        Bot.SetCurrentAction(sender, ChatAction.UploadAudio);
                        //        Bot.SendAudio(sender, System.IO.File.ReadAllBytes(audioFilePath),
                        //            Path.GetFileName(audioFilePath));
                        //        break;
                        //    }

                        //case "document":
                        //    {
                        //        string documentFilePath = Path.Combine(sampleData, "Document.txt");

                        //        Bot.SetCurrentAction(sender, ChatAction.UploadDocument);
                        //        Bot.SendDocument(sender, System.IO.File.ReadAllBytes(documentFilePath),
                        //            Path.GetFileName(documentFilePath));
                        //        break;
                        //    }

                        case "keyboard":
                            {
                                Bot.SendMessage(new SendMessageParams
                                {
                                    ChatId = sender.Id.ToString(),
                                    Text = "This is sample keyboard :",
                                    CustomKeyboard = new ReplyKeyboardMarkup
                                    {
                                        Keyboard = new List<List<KeyboardButton>>
                                {
                                    new List<KeyboardButton>
                                    {
                                        new KeyboardButton { Text="send location",RequestContact=false,RequestLocation=true }
                                        ,   new KeyboardButton {Text="cancel",RequestContact=false,RequestLocation=false }

                                    }
                                },
                                        ResizeKeyboard = true
                                    },
                                    ReplyToMessage = message
                                });

                                break;
                            }
                        case "cancel":
                            {
                                Bot.SendMessage(new SendMessageParams
                                {
                                    ChatId = sender.Id.ToString(),
                                    Text = $"You choose keyboard command : {message.Text}",
                                });
                                break;
                            }

                        default:
                            {
                                Bot.SendMessage(new SendMessageParams
                                {
                                    ChatId = sender.Id.ToString(),
                                    Text = "Comando non riconosciuto!",
                                });

                                break;
                            }
                    }
            }
            catch (Exception ex)
            {

            }
        }

        private static void GetLocationContactFromMessage(Message message, MessageSender sender)
        {
            if (message.Location != null)
            {
                Console.WriteLine($"Location :({message.Location.Latitude},{message.Location.Longitude})");
                Bot.SendMessage(new SendMessageParams
                {
                    ChatId = sender.Id.ToString(),
                    Text = $"You Send location",
                    ReplyToMessage = message
                });
            }
            else if (message.Contact != null)
            {
                Console.WriteLine($"Contact :({message.Contact.FirstName},{message.Contact.LastName},{message.Contact.PhoneNumber})");
                Bot.SendMessage(new SendMessageParams
                {
                    ChatId = sender.Id.ToString(),
                    Text = $"You Send Contact",
                    ReplyToMessage = message
                });
            }
        }

        public static void DownloadFileFromMessage(Message message, string savePath)
        {
            // Make storage path
            savePath = Path.Combine(savePath, "Storage");
            savePath = Path.Combine(savePath, message.From.Username ?? message.From.Id.ToString());
            if (!Directory.Exists(savePath))
                Directory.CreateDirectory(savePath);

            FileDownloadResult fileInfo = null;
            if (message.Document != null)
                fileInfo = Bot.DownloadFileById(message.Document.FileId, savePath);


            // Download video if exists
            if (message.Video != null)
                fileInfo = Bot.DownloadFileById(message.Video.FileId, savePath);


            // Download audio if exists
            if (message.Audio != null)
                fileInfo = Bot.DownloadFileById(message.Audio.FileId, savePath);


            // Download photo if exists
            if (message.Photo != null)
                foreach (PhotoSize photoSize in message.Photo)
                    fileInfo = Bot.DownloadFileById(photoSize.FileId, savePath);

            // Download sticker if exists
            if (message.Sticker != null)
                fileInfo = Bot.DownloadFileById(message.Sticker.FileId, savePath);
            //
            if (fileInfo != null)
                Console.WriteLine($"File : {fileInfo.FilePath} Size : {fileInfo.FileSize} was downloaded successfully");
        }
    }
}
