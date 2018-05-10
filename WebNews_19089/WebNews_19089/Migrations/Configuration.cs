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
                    Name="Politics"
                },
                new Categories {
                    ID=2,
                    Name="Sports"
                },
                new Categories {
                    ID=3,
                    Name="Video Games"
                },
                new Categories {
                    ID=4,
                    Name="Tech"
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
                }
            };
            photos.ForEach(pp => context.Photos.AddOrUpdate(p => p.ID, pp));
            context.SaveChanges();




        }
    }
}
