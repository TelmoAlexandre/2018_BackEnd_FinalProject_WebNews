namespace WebNews_19089.Migrations {
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WebNews_19089.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<WebNews_19089.Models.ApplicationDbContext> {
        public Configuration() {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(WebNews_19089.Models.ApplicationDbContext context) {

            // -------------------------------------------------------- 
            //                          Categories
            // --------------------------------------------------------

            var category = new List<Categories> {
                new Categories {
                    ID=1,
                    Name="World"
                },
                new Categories {
                    ID=2,
                    Name="Science"
                },
                new Categories {
                    ID=3,
                    Name="Tech"
                },
                new Categories {
                    ID=4,
                    Name="Video Games"
                }
            };
            category.ForEach(cc => context.Categories.AddOrUpdate(c => c.ID, cc));
            context.SaveChanges();

            // -------------------------------------------------------- 
            //                          News
            // --------------------------------------------------------

            var news = new List<News> {
                new News{
                    ID=1,
                    Title="US 'provocation' threatens peace, says North Korea",
                    Description="North Korea has warned the US about using \"pressure and military threats\" against it as the two countries prepare for a historic summit.",
                    Content="A Foreign Ministry official said the US was deliberately provoking the North by suggesting sanctions will not be lifted until it gives up nuclear weapons. US President Donald Trump and North Korean leader Kim Jong-un are due to meet in the next few weeks. It will be the first ever meeting between the two countries leaders. North and South Korean leaders agreed last month to denuclearise the region, at a border summit which came after months of warlike rhetoric from the North and Mr Trump. Mr Kim became the first North Korean leader to set foot in South Korea since the end of Korean hostilities in 1953.",
                    NewsDate = new DateTime(2018,05,06),
                    CategoryFK = 1
                },
                new News{
                    ID=2,
                    Title="Iran's Rouhani warns Trump of 'historic regret' over nuclear deal",
                    Description="Iranian President Hassan Rouhani has warned that the US will face \"historic regret\" if Donald Trump scraps the nuclear agreement with Tehran.",
                    Content="Mr Rouhani\'s comments come as the US president decides whether to pull out of the deal by a 12 May deadline. Mr Trump has strongly criticised the agreement, calling it \"insane\". The 2015 deal - between Iran, the US, China, Russia, Germany, France and the UK - lifted sanctions on Iran in return for curbs on its nuclear programme. France, the UK and Germany have been trying persuade the US president that the current deal is the best way to stop Iran developing nuclear weapons. British Foreign Secretary Boris Johnson is travelling to Washington on Sunday to discuss the matter with White House officials.",
                    NewsDate = new DateTime(2018,05,06),
                    CategoryFK = 1
                },
                new News{
                    ID=3,
                    Title="Indian engineers kidnapped in Afghanistan's Baghlan province",
                    Description="Seven Indian engineers have been kidnapped in Afghanistan along with their Afghan driver, police say.",
                    Content="Gunmen grabbed them from a vehicle on the outskirts of the Baghlan provincial capital, Pul-e Khomri, on Sunday. No group has said it carried out the kidnapping. However, provincial governor Abdul Hai Nemati told Tolo TV that the Taliban was responsible. Kidnappings are a serious problem in Afghanistan where large areas are blighted by gangs or militant groups. Provincial council chairman Mohammad Safdar Mohseni said the group had ignored warnings to take a police escort through an area largely controlled by the Taliban. Indian officials in Kabul said the engineers worked for the Da Afghanistan Breshna Sherkat company that operates a power station in northern Baghlan. \"We are in contact with the Afghan authorities and further details are being ascertained,\" a spokesman for Indian external affairs said. In 2011, 12 Iranian and Afghan engineers were kidnapped while working on a road project in western Afghanistan. They were released after local tribal elders acted as mediators with Taliban insurgents. Last year, a Finnish woman working for a Swedish aid group was kidnapped from a Kabul guesthouse and released some months later.",
                    NewsDate = new DateTime(2018,05,06),
                    CategoryFK = 1
                },
                new News{
                    ID=4,
                    Title="Gatwick Airport 'chaos': Southern say 'don't travel to Brighton'",
                    Description="Passengers said there was \"absolute chaos\" at Gatwick Airport because of overcrowding on rail replacement services on the Brighton mainline.",
                    Content="Southern is advising people not to travel to the coast as there are no direct trains from London due to engineering work. People are waiting about two hours to board replacement buses, National Rail said. Disruption is expected to last until the end of the day and into Monday. Southern posted on their website: \"There are currently large queues for the replacement bus services at Gatwick Airport and overcrowding at the station.\" As a result, customers should anticipate extended journey times and cancellations between London Victoria and Gatwick Airport to prevent further overcrowding. \"Services from Brighton towards London Victoria after 17:00 are expected to be extremely busy and journey times to be extended as a result.\"",
                    NewsDate = new DateTime(2018,05,06),
                    CategoryFK = 1
                },
                new News{
                    ID=5,
                    Title="Nigeria Kaduna: Bandits slaughter 51 villagers",
                    Description="A gang of what are said to be former cattle rustlers has killed at least 51 adults and children in a village in northern Nigeria, burning down homes.",
                    Content="Amongst the rows of dead bodies in Gwaska, in the Birnin Gwari area of Kaduna state, were children under the age of 10. Some bodies were mutilated. Survivors say the attackers surrounded Gwaska on Saturday afternoon. They set homes alight and fired shots, causing people to flee in panic - many straight towards the gunmen. Residents have demanded that President Muhammadu Buhari\'s government urgently deploy more police and military to protect vulnerable villages on the state border with Zamfara. Last month 14 miners were reportedly killed in an attack by gunmen in the Birnin Gwari area. Gwaska residents say Saturday\'s attackers used to be cattle thieves but had turned to banditry in the region\'s remote villages. The victims include members of a self-defence force, formed after attacks by well-armed cattle thieves.",
                    NewsDate = new DateTime(2018,05,06),
                    CategoryFK = 1
                },
                new News {
                    ID=6,
                    Title="UN puts brave face as climate talks get stuck",
                    Description="UN talks have been officially suspended as countries failed to resolve differences about implementing the Paris climate agreement.",
                    Content="The negotiations will resume in Bangkok in September where an extra week's meeting has now been scheduled. Delegates struggled with the complexity of agreeing a rulebook for the Paris climate pact that will come into force in 2020. Rows between rich and poor re - emerged over finance and cutting carbon. Overall progress at this meeting has been very slow, with some countries such as China looking to re - negotiate aspects of the Paris deal. UN climate chief Patricia Espinosa was putting a brave face on the talks. \"We face, I would say, a satisfactory outcome for this session but we have to be very, very clear that we have a lot of work in the months ahead,\" she said. \"We have to improve the pace of progress in order to be able to achieve a good outcome in Katowice in December,\" she said, referring to the end of year Conference of the Parties where the rulebook is due to completed and agreed. China and some other countries, perhaps frustrated by the slow pace, have sought in this Bonn meeting to go back to the position that existed before the 2015 deal, where only developed countries had to undertake to reduce their emissions.",
                    NewsDate= new DateTime(2018,05,10),
                    CategoryFK=2
                },
                new News {
                    ID=7,
                    Title="Workers banned from using USB sticks at IBM",
                    Description="Staff at IBM have been banned from using removable memory devices such as USB sticks, SD cards and flash drives.",
                    Content="The possibility of \"financial and reputational\" damage if staff lost or misused the devices prompted the decision, reported The Register. Instead, IBM staff who need to move data around will be encouraged to do so via an internal network. The decree banning removable storage acknowledges that complying with it could be \"disruptive\". IBM staff were told about the policy in an advisory from Shamla Naidoo, the company's global chief security officer. Some IBM departments had been banned from using removable portable media for some time, said Ms Naidoo, but now the decree was being implemented worldwide.IBM staff are expected to stop using removable devices by the end of May. When asked about the policy, an IBM spokeswoman said: \"We regularly review and enhance our security standards and practices to protect both IBM and our clients in an increasingly complex threat environment.\" Security expert Kevin Beaumont said: \"It is a brave move by IBM, as USB devices do present a real risk - often it is very easy to extract data from a company via these devices, and introduce malicious software.\" However, he said, IBM may face problems implementing its plan.", 
                    NewsDate= new DateTime(2018,05,10),
                    CategoryFK=3
                },
                new News {
                    ID=8,
                    Title="God of War Update Version 1.21 Is Live; Here’s What It Does",
                    Description="Sony Santa Monica Studio has released the photo mode update for God of War PS4. They originally announced that God of War will get patch verison 1.20 but the actual update that is live today is for 1.21 version of the game.",
                    Content ="Among the new additions are the Photo Mode feature and tweaks to the game’s text size. Fans were complaining of the small text size since the launch of the game so the new update now makes it easier for those who have a small screen to more easily see the text now. The download size for this update is around 450 MB and it is currently available to download now on the PS4. Some of the changes that are being mentioned by the users include access to the final weapon upgrades before finishing the game. Previously, you weren’t able to purchase the last upgrades for you weapons until you finished the game while it is possible to do so now. God of War is available now exclusively for the PS4. The game was developed by Sony Santa Monica Studio and published by Sony Interactive Entertainment.",
                    NewsDate = new DateTime(2018,05,10),
                    CategoryFK=4
                }
            };
            news.ForEach(aa => context.News.AddOrUpdate(a => a.Title, aa));
            context.SaveChanges();

            // -------------------------------------------------------- 
            //                          Users
            // --------------------------------------------------------

            var user = new List<UsersProfile> {
                new UsersProfile {
                    ID=1,
                    Name="Test1",
                    Birthday=new DateTime(1950,01,01),
                    Bio="Nulla blandit feugiat dui eget gravida. Phasellus fermentum euismod congue. Proin lorem quam, placerat nec justo et, consectetur tincidunt justo.",
                    UserName="Test1@mail.com"
                },
                new UsersProfile {
                    ID=2,
                    Name="Test2",
                    Birthday=new DateTime(1975,11,23),
                    Bio="interdum iaculis augue. Nullam ac tellus vestibulum, porttitor magna vel, fringilla sem. Integer at tincidunt odio, nec bibendum elit.",
                    UserName="Test2@mail.com"
                },
                new UsersProfile {
                    ID=3,
                    Name="Test3",
                    Birthday=new DateTime(1980,03,05),
                    Bio="Morbi eu massa lorem. Integer aliquet leo ut dui aliquam dictum. Integer vulputate viverra ante sed vestibulum. Nullam ut malesuada arcu.",
                    UserName="Test3@mail.com"
                },
                new UsersProfile {
                    ID=4,
                    Name="Test4",
                    Birthday=new DateTime(1943,02,13),
                    Bio="Maecenas erat risus, dignissim eget laoreet at, porttitor vitae risus. Nunc aliquet interdum lacus nec consequat. Cras dui ipsum, efficitur a massa sit amet.",
                    UserName="Test4@mail.com"
                }
            };
            user.ForEach(uu => context.UsersProfile.AddOrUpdate(u => u.ID, uu));
            context.SaveChanges();

            // -------------------------------------------------------- 
            //                          Comments
            // --------------------------------------------------------
            var comments = new List<Comments> {
                new Comments {
                    ID=1,
                    Content="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque molestie consectetur ligula, nec pellentesque eros convallis vel. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.",
                    CommentDate=new DateTime(2018,05,06),
                    NewsFK=1,
                    UserProfileFK=1
                },
                new Comments {
                    ID=2,
                    Content="Sed non enim in tortor ultrices ultrices. Praesent venenatis lectus vel vehicula cursus. Integer velit diam, tempor nec tincidunt nec, elementum nec nisi.",
                    CommentDate=new DateTime(2018,05,06),
                    NewsFK=2,
                    UserProfileFK=2
                },
                new Comments {
                    ID=3,
                    Content="Nunc a arcu sapien. Sed convallis dignissim ligula eu dignissim. Sed et odio vel odio semper efficitur. Nulla finibus, erat non ornare tincidunt.",
                    CommentDate=new DateTime(2018,05,06),
                    NewsFK=4,
                    UserProfileFK=3
                },
                new Comments {
                    ID=4,
                    Content="Nulla nec lectus sagittis, congue dui a, bibendum nunc. Nullam porta, lacus vel imperdiet rhoncus, felis orci aliquet urna, id condimentum leo nibh quis nisi.",
                    CommentDate=new DateTime(2018,05,07),
                    NewsFK=4,
                    UserProfileFK=2
                },
                new Comments {
                    ID=5,
                    Content="Nunc a arcu sapien. Sed convallis dignissim ligula eu dignissim. Sed et odio vel odio semper efficitur. Nulla finibus, erat non ornare tincidunt, augue nulla feugiat ante, eget ultrices diam nibh sit amet arcu. Morbi fringilla porttitor tincidunt.",
                    CommentDate=new DateTime(2018,05,08),
                    NewsFK=4,
                    UserProfileFK=3
                }
            };
            comments.ForEach(cc => context.Comments.AddOrUpdate(c => c.ID, cc));
            context.SaveChanges();

            // -------------------------------------------------------- 
            //                       Photos
            // --------------------------------------------------------
            var photos = new List<Photos> {
                new Photos {
                    ID=1,
                    Name="News1",
                    NewsFK=1
                },
                new Photos {
                    ID=2,
                    Name="News2",
                    NewsFK=2
                },
                new Photos {
                    ID=3,
                    Name="News3",
                    NewsFK=3
                },
                new Photos {
                    ID=4,
                    Name="News4",
                    NewsFK=4
                },
                new Photos {
                    ID=5,
                    Name="News5",
                    NewsFK=5
                },
                new Photos {
                    ID=6,
                    Name="News6",
                    NewsFK=6
                },
                new Photos {
                    ID=7,
                    Name="News7",
                    NewsFK=7
                },
                new Photos {
                    ID=8,
                    Name="News8",
                    NewsFK=8
                }
            };
            photos.ForEach(pp => context.Photos.AddOrUpdate(p => p.ID, pp));
            context.SaveChanges();




        }
    }
}
