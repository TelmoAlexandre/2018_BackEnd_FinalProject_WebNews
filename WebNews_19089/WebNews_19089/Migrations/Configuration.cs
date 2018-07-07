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
                }
            };
            category.ForEach(cc => context.Categories.AddOrUpdate(c => c.ID, cc));
            context.SaveChanges();

            // -------------------------------------------------------- 
            //                          Users
            // --------------------------------------------------------

            var user = new List<UsersProfile> {
                new UsersProfile {
                    ID=1,
                    Name="Journalist1",
                    Birthday=new DateTime(1950,01,01),
                    UserName="journalist1@mail.com"
                },
                new UsersProfile {
                    ID=2,
                    Name="Journalist2",
                    Birthday=new DateTime(1975,11,23),
                    UserName="journalist2@mail.com"
                },
                new UsersProfile {
                    ID=3,
                    Name="Journalist3",
                    Birthday=new DateTime(1980,03,05),
                    UserName="journalist3@mail.com"
                },
                new UsersProfile {
                    ID=4,
                    Name="Admin",
                    Birthday=new DateTime(1985,07,12),
                    UserName="admin@mail.com"
                },
                new UsersProfile {
                    ID=5,
                    Name="Editor",
                    Birthday=new DateTime(1952,05,20),
                    UserName="editor@mail.com"
                }
            };
            user.ForEach(uu => context.UsersProfile.AddOrUpdate(u => u.ID, uu));
            context.SaveChanges();

            // -------------------------------------------------------- 
            //                          News
            // --------------------------------------------------------

            var news = new List<News> {
                new News{
                    ID=1,
                    Title="US 'provocation' threatens peace, says North Korea",
                    Description="North Korea has warned the US about using \"pressure and military threats\" against it as the two countries prepare for a historic summit.",
                    Content="A Foreign Ministry official said the US was deliberately provoking the North by suggesting sanctions will not be lifted until it gives up nuclear weapons. <br/><br/> US President Donald Trump and North Korean leader Kim Jong-un are due to meet in the next few weeks. <br/><br/> It will be the first ever meeting between the two countries leaders. <br/><br/> North and South Korean leaders agreed last month to denuclearise the region, at a border summit which came after months of warlike rhetoric from the North and Mr Trump. <br/><br/> Mr Kim became the first North Korean leader to set foot in South Korea since the end of Korean hostilities in 1953.",
                    NewsDate = new DateTime(2018,05,06,11,0,0),
                    CategoryFK = 1,
                    UsersProfileList = new List<UsersProfile> { user[0], user[2] }
                },
                new News{
                    ID=2,
                    Title="Iran's Rouhani warns Trump of 'historic regret' over nuclear deal",
                    Description="Iranian President Hassan Rouhani has warned that the US will face \"historic regret\" if Donald Trump scraps the nuclear agreement with Tehran.",
                    Content="Mr Rouhani\'s comments come as the US president decides whether to pull out of the deal by a 12 May deadline. <br/><br/> Mr Trump has strongly criticised the agreement, calling it \"insane\". The 2015 deal - between Iran, the US, China, Russia, Germany, France and the UK - lifted sanctions on Iran in return for curbs on its nuclear programme. <br/><br/> France, the UK and Germany have been trying persuade the US president that the current deal is the best way to stop Iran developing nuclear weapons. <br/><br/> British Foreign Secretary Boris Johnson is travelling to Washington on Sunday to discuss the matter with White House officials.",
                    NewsDate = new DateTime(2018,05,06,10,0,0),
                    CategoryFK = 1,
                    UsersProfileList = new List<UsersProfile> { user[0]}
                },
                new News{
                    ID=3,
                    Title="Indian engineers kidnapped in Afghanistan's Baghlan province",
                    Description="Seven Indian engineers have been kidnapped in Afghanistan along with their Afghan driver, police say.",
                    Content="Gunmen grabbed them from a vehicle on the outskirts of the Baghlan provincial capital, Pul-e Khomri, on Sunday. No group has said it carried out the kidnapping. However, provincial governor Abdul Hai Nemati told Tolo TV that the Taliban was responsible. Kidnappings are a serious problem in Afghanistan where large areas are blighted by gangs or militant groups. <br/><br/> Provincial council chairman Mohammad Safdar Mohseni said the group had ignored warnings to take a police escort through an area largely controlled by the Taliban. <br/><br/> Indian officials in Kabul said the engineers worked for the Da Afghanistan Breshna Sherkat company that operates a power station in northern Baghlan. <br/><br/> \"We are in contact with the Afghan authorities and further details are being ascertained,\" a spokesman for Indian external affairs said. In 2011, 12 Iranian and Afghan engineers were kidnapped while working on a road project in western Afghanistan. They were released after local tribal elders acted as mediators with Taliban insurgents. Last year, a Finnish woman working for a Swedish aid group was kidnapped from a Kabul guesthouse and released some months later.",
                    NewsDate = new DateTime(2018,05,06,9,0,0),
                    CategoryFK = 1,
                    UsersProfileList = new List<UsersProfile> { user[1], user[2] }
                },
                new News{
                    ID=4,
                    Title="Gatwick Airport 'chaos': Southern say 'don't travel to Brighton'",
                    Description="Passengers said there was \"absolute chaos\" at Gatwick Airport because of overcrowding on rail replacement services on the Brighton mainline.",
                    Content="Southern is advising people not to travel to the coast as there are no direct trains from London due to engineering work. <br/><br/> People are waiting about two hours to board replacement buses, National Rail said. <br/><br/> Disruption is expected to last until the end of the day and into Monday. <br/><br/> Southern posted on their website: \"There are currently large queues for the replacement bus services at Gatwick Airport and overcrowding at the station.\" <br/><br/> As a result, customers should anticipate extended journey times and cancellations between London Victoria and Gatwick Airport to prevent further overcrowding. <br/><br/> \"Services from Brighton towards London Victoria after 17:00 are expected to be extremely busy and journey times to be extended as a result.\"",
                    NewsDate = new DateTime(2018,05,06,8,0,0),
                    CategoryFK = 1,
                    UsersProfileList = new List<UsersProfile> { user[1]}
                },
                new News{
                    ID=5,
                    Title="Nigeria Kaduna: Bandits slaughter 51 villagers",
                    Description="A gang of what are said to be former cattle rustlers has killed at least 51 adults and children in a village in northern Nigeria, burning down homes.",
                    Content="Amongst the rows of dead bodies in Gwaska, in the Birnin Gwari area of Kaduna state, were children under the age of 10. Some bodies were mutilated. Survivors say the attackers surrounded Gwaska on Saturday afternoon. They set homes alight and fired shots, causing people to flee in panic - many straight towards the gunmen. Residents have demanded that President Muhammadu Buhari\'s government urgently deploy more police and military to protect vulnerable villages on the state border with Zamfara. Last month 14 miners were reportedly killed in an attack by gunmen in the Birnin Gwari area. Gwaska residents say Saturday\'s attackers used to be cattle thieves but had turned to banditry in the region\'s remote villages. The victims include members of a self-defence force, formed after attacks by well-armed cattle thieves.",
                    NewsDate = new DateTime(2018,05,06,7,0,0),
                    CategoryFK = 1,
                    UsersProfileList = new List<UsersProfile> { user[2]}
                },
                new News {
                    ID=6,
                    Title="UN puts brave face as climate talks get stuck",
                    Description="UN talks have been officially suspended as countries failed to resolve differences about implementing the Paris climate agreement.",
                    Content="The negotiations will resume in Bangkok in September where an extra week's meeting has now been scheduled. Delegates struggled with the complexity of agreeing a rulebook for the Paris climate pact that will come into force in 2020. Rows between rich and poor re - emerged over finance and cutting carbon. Overall progress at this meeting has been very slow, with some countries such as China looking to re - negotiate aspects of the Paris deal. UN climate chief Patricia Espinosa was putting a brave face on the talks. \"We face, I would say, a satisfactory outcome for this session but we have to be very, very clear that we have a lot of work in the months ahead,\" she said. \"We have to improve the pace of progress in order to be able to achieve a good outcome in Katowice in December,\" she said, referring to the end of year Conference of the Parties where the rulebook is due to completed and agreed. China and some other countries, perhaps frustrated by the slow pace, have sought in this Bonn meeting to go back to the position that existed before the 2015 deal, where only developed countries had to undertake to reduce their emissions.",
                    NewsDate= new DateTime(2018,05,10,15,0,0),
                    CategoryFK=2,
                    UsersProfileList = new List<UsersProfile> { user[2], user[0] }
                },
                new News {
                    ID=7,
                    Title="Workers banned from using USB sticks at IBM",
                    Description="Staff at IBM have been banned from using removable memory devices such as USB sticks, SD cards and flash drives.",
                    Content="The possibility of \"financial and reputational\" damage if staff lost or misused the devices prompted the decision, reported The Register. Instead, IBM staff who need to move data around will be encouraged to do so via an internal network. The decree banning removable storage acknowledges that complying with it could be \"disruptive\". IBM staff were told about the policy in an advisory from Shamla Naidoo, the company's global chief security officer. Some IBM departments had been banned from using removable portable media for some time, said Ms Naidoo, but now the decree was being implemented worldwide.IBM staff are expected to stop using removable devices by the end of May. When asked about the policy, an IBM spokeswoman said: \"We regularly review and enhance our security standards and practices to protect both IBM and our clients in an increasingly complex threat environment.\" Security expert Kevin Beaumont said: \"It is a brave move by IBM, as USB devices do present a real risk - often it is very easy to extract data from a company via these devices, and introduce malicious software.\" However, he said, IBM may face problems implementing its plan.", 
                    NewsDate= new DateTime(2018,05,10,18,0,0),
                    CategoryFK=3,
                    UsersProfileList = new List<UsersProfile> { user[2]}
                },
                new News {
                    ID=8,
                    Title="Banks told to reveal tech meltdown plans",
                    Description="UK banks have been told to explain how they would cope with a technology failure or cyber-attack.",
                    Content="The Bank of England and the Financial Conduct Authority have given financial firms three months to detail how they would respond if their systems failed. Some TSB customers were left unable to access online banking for more than a month following a botched systems upgrade in April. Banks could be ordered to take action if their plans are judged to be poor. The Bank of England and FCA have emphasised that senior management at banks will be held accountable for prolonged disruption to services. The two organisations have launched a consultation seeking the views of customers as well as banks, insurers and other financial institutions. The regulators have warned that upgrading computer systems to match services provided by newer financial start-ups could lead to service disruption. In certain conditions, they have suggested that two days is an acceptable limit for disruption to service. <br/><br/> \"Operational disruption can impact financial stability, threaten the viability of individual firms and financial market infrastructures, or cause harm to consumers,\" said FCA chief executive Andrew Bailey and the Bank of England's Jon Cunliffe, in a statement. If the contingency plans put forward by banks and other financial institutions are judged to be unsuitable, they could be ordered to make their systems more resilient.",
                    NewsDate= new DateTime(2018,06,05,12,55,0),
                    CategoryFK=3,
                    UsersProfileList = new List<UsersProfile> { user[1], user[2]}
                },
                new News {
                    ID=9,
                    Title="Dark web sting leads to arrest of alleged spyware thief",
                    Description="A programmer who allegedly tried to sell stolen surveillance tools worth $50m (£38m) on the dark web has been caught and charged in Israel.",
                    Content="The software was reportedly stolen from a security firm called NSO Group, known for creating surveillance software. The former NSO developer is believed to have stolen the code after learning he was going to be sacked. Israel's Justice Ministry said if the sale had gone ahead it could have harmed state security. In a statement, the ministry said the accused was a senior programmer at NSO who had access to the firm's development systems including its stores of source code. The ministry said once the programmer had downloaded company code, he posed as a hacker and sought to sell the software on dark web markets. It is claimed that he asked for a payment of $50m to be made in crypto-cash to make it harder to trace. The unidentified individual who agreed to buy the software contacted NSO before the deal went ahead. A sting was set up and the suspected thief was arrested. Daily newspaper Israel Hayom reported that the indictment against the former NSO employee said: \"The defendant's ‎actions seriously jeopardised the NSO Group and ‎could have led to its collapse.\". The programmer has been charged with theft, intent to compromise national security, pursuing an unlicensed defence transaction and disruption of a computer system. A statement released by the defendant's legal team said its client \"never tried to undermine national security\". <br/><br/>They added: ‎\"We believe the court will get to the truth in this ‎case. We will prove these allegations to be ‎disproportionate and baseless.\" NSO ‎Group came to prominence in 2016 after being accused of creating software that can overcome security protections on Apple iPhones. NSO software has also been implicated in a long-running spyware scandal in Mexico. In response, NSO has said it only sells to authorised agencies and has no control over the way its tools are used.",
                    NewsDate= new DateTime(2018,06,01,09,0,0),
                    CategoryFK=3,
                    UsersProfileList = new List<UsersProfile> { user[0], user[2]}
                },
                new News {
                    ID=10,
                    Title="Fetch rover! Robot to retrieve Mars rocks",
                    Description="UK engineers will design a robot that can retrieve rock samples on Mars so they can be sent to Earth for study.",
                    Content="The European Space Agency is issuing contracts to industry to spec the technology needed for what will be a complex joint undertaking with the US. Aerospace giant Airbus will scope the concept for a surface \"fetch rover\" at its Stevenage centre north of London. <br/><br/>Esa and the American space agency (Nasa) expect to send the sample-return equipment to the Red Planet in 2026. \"It will be a relatively small rover - about 130kg; but the requirements are very demanding,\" said Ben Boyes who will lead the feasibility team at Airbus. \"The vehicle will have to cover large distances using a high degree of autonomy, planning its own path ahead day after day,\" he told BBC News. Esa and Nasa signed a letter of intent in April committing themselves to bringing back pieces of Martian rock and soil to Earth before the end of the next decade. It will be a daring venture that will be done in stages and take several years to complete. <br/><br/>Why does this matter? Numerous satellites have been sent to study Mars from above, and seven landers have so far touched down to sift its surface materials. But scientists say some of the biggest questions about the planet - such as whether it has ever hosted life - can only really be answered if rock and soil is brought to Earth. No amount of miniaturisation can give Martian landers the capabilities of the most modern analytical tools now available in research laboratories. Mars Sample Return is the next big thing in planetary science.",
                    NewsDate= new DateTime(2018,06,07,18,0,0),
                    CategoryFK=2,
                    UsersProfileList = new List<UsersProfile> { user[0]}
                },
                new News {
                    ID=11,
                    Title="Japan floods: Dozens killed in deluges and landslides",
                    Description="Flooding and landslides have killed at least 50 people and left dozens missing in western areas of Japan.",
                    Content="Most of the deaths have occurred in Hiroshima prefecture, which has been hit by torrential rain since Thursday. Hundreds of homes have been damaged. About 1.5 million people have been ordered to leave their homes and three million more advised to do so. Thousands of police, firefighters and soldiers are taking part in search-and-rescue operations. <br/><br/>Some of the victims have been buried alive by landslides, Japan's Kyodo news agency reports. In the town of Motoyama, about 600km (370 miles) west of the capital Tokyo, 583mm (23in) of rain fell between Friday morning and Saturday morning, Japan's meteorological agency said. More rain is expected over the next few days. Kyoto, about 300km to the east of Hiroshima, has also been hit by downpours. Local resident Manabu Takeshita told the Japan Times website: \"Anybody living near the river has got to be nervous because typhoon season is just really starting.\"",
                    NewsDate= new DateTime(2018,06,07,11,24,0),
                    CategoryFK=1,
                    UsersProfileList = new List<UsersProfile> { user[1], user[0]}
                },
                new News {
                    ID=12,
                    Title="South Korean women protest in Seoul over hidden sex cameras",
                    Description="Tens of thousands of women gathered in Seoul on Saturday calling for a crackdown on spy cam pornography, in one of the country's biggest ever female-only protests.",
                    Content="Perpetrators film or photograph women with hidden cameras in public spaces. Although distributing pornography is illegal in South Korea, the videos and pictures are shared widely online. Organisers say women live in constant fear of being photographed or filmed without their knowledge. <br/><br/>Carrying placards and banners with messages like \"My life is not your porn\", the women were mostly teenagers or in their 20s - seen as the main victims of the hidden cameras. \"Those men who film such videos! Those who upload them! Those who watch them! All of them should be punished sternly!\" they chanted. The women covered their faces with masks, hats and sunglasses as instructed by the organisers. <br/><br/>Demonstrators said around 55,000 women took part, although police put the figure at around 20,000. The recent protests began after police arrested a 25-year-old woman in May for secretly photographing a male colleague who posed nude for university art students. She then shared the picture online.",
                    NewsDate= new DateTime(2018,06,07,22,0,0),
                    CategoryFK=1,
                    UsersProfileList = new List<UsersProfile> { user[2]}
                },
                new News {
                    ID=13,
                    Title="US service member killed in 'insider attack' in Afghanistan",
                    Description="A US service member has been killed and two others injured in what appears to be an insider attack in Afghanistan, Nato says.",
                    Content="The attack took place in southern Afghanistan on Saturday, a statement from the Nato-led Resolute Support mission said. It was not immediately clear who carried out the attack, or whether they had been apprehended. However, the statement said it was \"an apparent insider attack\". <br/><br/>The two wounded service members are in a \"stable\" condition, Nato said, adding it would not be releasing any further details until next-of-kin had been informed. But a police officer told news agency AFP the shooting happened at the airport in Tarinkot, the capital of Uruzgan province - although they were unable to confirm this detail with any Nato officials. The Taliban have also released a statement, saying an Afghan soldier opened fire on US soldiers in Uruzgan. The attack comes a year after three US special forces soldiers were shot dead by one of their Afghan colleagues. It was claimed by the Taliban. <br/><br/>However, these kind of attacks - where local troops turn on international forces and often referred to as \"green on blue\" incidents - are not necessarily the work of militants infiltrating the security forces. Cultural misunderstandings and combat stress have also been named as the cause in previous attacks. The US still has an estimated 14,000 troops in the country helping support the Afghan military, despite Nato's combat mission in Afghanistan officially ended in 2014.",
                    NewsDate= new DateTime(2018,06,06,02,0,0),
                    CategoryFK=1,
                    UsersProfileList = new List<UsersProfile> { user[2], user[1] }
                },
                new News {
                    ID=14,
                    Title="Call to turn oil rigs into nature reserves",
                    Description="Marine wildlife could benefit if some de-commissioned oil rigs were left on the sea bed, a survey says.",
                    Content="This challenges the conventional wisdom that the sea bed should be restored to its pristine state when a rig's life ends. The paper says over the 30-year lifetime of an oil rig, creatures have often colonised the structure to form a reef. It says this artificial habitat can be more valuable than the original seabed. It can also protect sea creatures from fishing. <br/><br/>The paper from the University of Technology in Sydney, Australia, is based on a survey of 40 experts from academia, government and consultancies. Their focus was on the North Sea - but the authors say the principles are applicable anywhere. More than 90% of the experts surveyed said governments should abandon the principle that oil rigs should always be removed. Instead, there should be a more flexible, case-by-case approach to de-commissioning. <br/><br/>It warns that the process of removing the rigs can be damaging to the environment in its own right. One of the partners in the new report was North Sea Futures, run by an environment consultant, Anna-Mette Jorgensen. She told BBC News: \"Current policy is very unlikely to be good for the environment or the taxpayer. The ecosystem has adapted to these structures. There are lots of fish round them - and they are one of very few areas protected from fisheries.\" She said the endangered lophelia cold water coral in the North Sea appeared to be using the rigs to colonise new areas.",
                    NewsDate= new DateTime(2018,06,02,06,14,0),
                    CategoryFK=2,
                    UsersProfileList = new List<UsersProfile> { user[0] }
                },
                new News {
                    ID=15,
                    Title="Polluters exposed by new eye in the sky satellite",
                    Description="What must it be like to live in the Siberian town of Norilsk on a \"bad air day\"?",
                    Content="They say the local smelting industry produces 1% of all the sulphur dioxide (SO₂) going into the air globally, something close to two million tonnes a year. SO2 is particularly unpleasant if breathed in; but it also washes out of the sky as \"acid rain\", damaging plant-life and denuding the quality of water in streams and rivers. The extent of Norilsk's pollution problem is captured in remarkable new maps from Europe's Sentinel-5P satellite. The spacecraft was put up last year to track the gases responsible for dirty air - with SO₂ being one of the prime culprits. <br/><br/>Assembled in the UK and carrying the Dutch-led Tropomi instrument, S5P promises to be a game-changer in monitoring what's happening in our atmosphere. It has much higher resolution than its predecessors and acquires data on such a scale that its maps can be assembled very quickly. \"What's very interesting about the Norilsk data is that they show you the different transport pathways,\" explained Dr Nicolas Theys from the Royal Belgian Institute for Space Aeronomy (BIRA-IASB). \"You can see how the emissions follow the topography, moving around the mountains. People could use this information to better assess the environmental impact in this region.\"",
                    NewsDate= new DateTime(2018,05,28,13,42,0),
                    CategoryFK=2,
                    UsersProfileList = new List<UsersProfile> { user[1]}
                },
                new News {
                    ID=16,
                    Title="Foam pollution kills fish in River Great Ouse",
                    Description="More than 2,000 fish have died after pollution left a river looking like a bubble bath.",
                    Content="The foam was first spotted in the River Great Ouse in Brackley, Northamptonshire on Friday, 29 June and travelled onto Buckingham. The Environment Agency said the unidentified substance has now \"sufficiently diluted\" and is no longer \"causing any issues\". <br/><br/>One Buckingham resident described it as like a \"washing machine has exploded\". Agency officers have provided the casualty figure, but believe the number of dead fish could be higher as the \"pollution has severely impacted the river's ecosystem\". They said it may take \"years\" for the river to recover. <br/><br/>The Environment Agency is investigating the incident, and a spokesman said they were waiting for sample results to confirm the pollutants, which may contain detergents. He said: \"We believe that the pollutant has now passed through Stony Stratford and into Milton Keynes with no immediate adverse impacts on aquatic life past Thornton, Buckinghamshire.\" They do not believe there is a risk to humans or animals, but as a precaution are asking people to stay out of the river and keep pets and livestock away from the stretch between Brackley and Milton Keynes until the investigation is complete. <br/><br/>Anglers are also being asked not to fish in the polluted areas. After Buckingham, the 140 mile (230km) long river flows through Milton Keynes, Bedford, St Neots, Huntingdon, St Ives, Ely and The Fens before entering The Wash at King's Lynn. ",
                    NewsDate= new DateTime(2018,05,14,11,15,0),
                    CategoryFK=2,
                    UsersProfileList = new List<UsersProfile> { user[1], user[0]}
                },
                new News {
                    ID=17,
                    Title="YouTuber in row over copyright infringement of his own song",
                    Description="Paul Davids thought he had seen it all when it came to YouTube's copyright protection system.",
                    Content="The Dutch YouTuber's most popular videos include him playing famous guitar riffs, comparing different instruments and teaching various guitar skills and techniques. \"Just like probably all the music YouTubers out there,\" he explained in a video to his 625,000 subscribers, \"once in a while I get an email stating I'm infringing on someone's copyrighted material.\" Paul had been contacted by YouTube to advise him that one of his videos had been flagged for copyright infringement, but in his own words, \"this was a little different\". <br/><br/>The copyright he had apparently infringed upon was his own. \"It said what song I was infringing on, and what I found was quite shocking,\" said Paul. \"Someone took my track, added vocals and guitar to make their own track, and uploaded it to YouTube, but I got the copyright infringement notice!\" <br/><br/>Paul had been accused of plagiarising his own music - and worse, all the money that video was earning would now be directed towards the person who copied his content.",
                    NewsDate= new DateTime(2018,06,03,19,23,0),
                    CategoryFK=3,
                    UsersProfileList = new List<UsersProfile> { user[0]}
                },
                new News {
                    ID=18,
                    Title="The world's first family to live in a 3D-printed home",
                    Description="A family in France has become the first in the world to move into a 3D-printed house. The four-bedroom property is a prototype for bigger projects aiming to make housebuilding quicker and cheaper. Could it cause a shift in the building industry?",
                    Content="With curved walls designed to reduce the effects of humidity and digital controls for disabled people, this house could be an expensive realisation of an architect's vision. But having taken 54 hours to print - with four more months for contractors to add in things such as windows, doors and the roof - its cost of around £176,000 to build makes it 20% cheaper than an identical construction using more traditional solutions. The team now believe they could print the same house again in only 33 hours. The 95m (1022ft) square house - built for a family of five with four bedrooms and a big central space in Nantes - is a collaboration between the city council, a housing association and University of Nantes. <br/><br/>Francky Trichet, the council's lead on technology and innovation, says the purpose of the project was to see whether this type of construction could become mainstream for housing, and whether its principles could be applied to other communal buildings, such as sports halls. He believes the process will disrupt the construction industry. \"For 2,000 years there hasn't been a change in the paradigm of the construction process. We wanted to sweep this whole construction process away,\" he says. \"That's why I'm saying that we're at the start of a story. We've just written, 'Once upon a time'.\" <br/><br/>Now, he says, their work will \"force\" private companies to \"take the pen\" and continue the narrative. The house has been built in a deprived neighbourhood in the north of the town and was partly funded by the council. Nordine and Nouria Ramdani, along with their three children, were the lucky ones chosen to live there. <br/><br/>\"It's a big honour to be a part of this project,\" says Nordine. \"We lived in a block of council flats from the 60s, so it's a big change for us. \"It's really something amazing to be able to live in a place where there is a garden, and to have a detached house.\" <br/><br/>The house is designed in a studio by a team of architects and scientists, then programmed into a 3D printer. The printer is then brought to the site of the home. It works by printing in layers from the floor upwards. Each wall consists of two layers of the insulator polyurethane, with a space in-between which is filled with cement. <br/><br/>This creates a thick, insulated, fully-durable wall. The windows, doors, and roof are then fitted. And, voila, you have a home. The house was the brainchild of Benoit Furet, who heads up the project at University of Nantes. He thinks that in five years they will reduce the cost of the construction of such houses by 25% while adhering to building regulations, and by 40% in 10 to 15 years. This is partly because of the technology becoming more refined and cheaper to develop and partly because of economies of scale as more houses are built. Printing, he adds, also allows architects to be far more creative with the shapes of the houses they are building. For example, the house in Nantes was built to curve around the 100-year-old protected trees on the plot.",
                    NewsDate= new DateTime(2018,06,04,21,11,0),
                    CategoryFK=3,
                    UsersProfileList = new List<UsersProfile> { user[2], user[1] }
                },
                new News {
                    ID=19,
                    Title="Baidu's self-drive buses enter 'mass production'",
                    Description="One of China's biggest technology companies has declared it has begun mass production of a self-driving bus.",
                    Content="Baidu made the announcement after building its 100th Apolong vehicle at its factory in the country's south-eastern Fujian province. It said the vehicles would initially be put to commercial use within Chinese cities but added it was also targeting foreign markets. The company is one of several competing to sell \"level-4 autonomy\" buses. The classification - set by the transport engineering body SAE International - refers to highly automated driving systems that can cope with most driving conditions, even if a human fails to respond appropriately to a request to intervene. <br/><br/>It is one step below the maximum level-5 tier, which extends to all driving scenarios, including dirt roads and unusual weather conditions. Baidu's chief executive, Robin Li, detailed its plans at the company's annual artificial intelligence developer conference in Beijing. He said: \"2018 marks the first year of commercialisation for autonomous driving. \"In the past, China exported cheap commodities to the world. In the future, China will export AI technology to the world.\" The Apolong bus can seat up to 14 people, and has been developed with a local vehicle manufacturer. <br/><br/>It has no driver's seat, steering wheel or pedals. It runs on electric power and can travel up to 100km (62 miles) after a two-hour charge, at up to 70km/h. Baidu envisages it being used for \"last-mile\" drop-offs within enclosed areas, such as airports and tourist sites. The company said partners would soon put it to use in Beijing, Shenzhen, Wuhan and other Chinese cities.",
                    NewsDate= new DateTime(2018,06,05,18,54,23),
                    CategoryFK=3,
                    UsersProfileList = new List<UsersProfile> { user[1], user[0], user[2] }
                },
                new News {
                    ID=20,
                    Title="E-waste mining could be big business - and good for the planet.",
                    Description="Many millions of tonnes of televisions, phones and other electronic equipment are discarded each year, despite them being a rich source of metals. But now e-waste mining has the potential to become big business.",
                    Content="Professor Veena Sahajwalla's mine in Australia produces gold, silver and copper - and there isn't a pick-axe in sight. Her \"urban mine\" at the University of New South Wales (UNSW) is extracting these materials not from rock, but from electronic gadgets. The Sydney-based expert in materials science reckons her operation will become efficient enough to be making a profit within a couple of years. <br/><br/>\"Economic modelling shows the cost of around $500,000 Australian dollars (£280,000) for a micro-factory pays off in two to three years, and can generate revenue and create jobs,\" she says. \"That means there are environmental, social and economic benefits.\" In fact, research indicates that such facilities can actually be far more profitable than traditional mining. <br/><br/>According to a study published recently in the journal Environmental Science & Technology, a typical cathode-ray tube TV contains about 450g of copper and 227g of aluminium, as well as around 5.6g of gold. While a gold mine can generate five or six grammes of the metal per tonne of raw material, that figure rises to as much as 350g per tonne when the source is discarded electronics. The figures emerged in a joint study from Beijing's Tsinghua University and Macquarie University, in Sydney, where academics examined data from eight recycling companies in China to work out the cost for extracting these metals from electronic waste. <br/><br/>Expenses included the costs of waste collection, labour, energy, material and transportation, as well as capital costs for the recyclers' equipment and buildings. And when these costs - and the effects of Chinese government subsidies for recycling - were taken into account, the team found that mining from ore was 13 times more expensive than e-waste mining.",
                    NewsDate= new DateTime(2018,06,06,23,34,0),
                    CategoryFK=3,
                    UsersProfileList = new List<UsersProfile> { user[0]}
                }
            };
            news.ForEach(aa => context.News.AddOrUpdate(a => a.Title, aa));
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
                    ID=2,
                    Content="Nulla nec lectus sagittis, congue dui a, bibendum nunc. Nullam porta, lacus vel imperdiet rhoncus, felis orci aliquet urna, id condimentum leo nibh quis nisi.",
                    CommentDate=new DateTime(2018,05,07),
                    NewsFK=4,
                    UserProfileFK=2
                },
                new Comments {
                    ID=1,
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
                    Name="News1.jpg",
                    NewsFK=1
                },
                new Photos {
                    ID=2,
                    Name="News2.jpg",
                    NewsFK=2
                },
                new Photos {
                    ID=3,
                    Name="News3.jpg",
                    NewsFK=3
                },
                new Photos {
                    ID=4,
                    Name="News4.jpg",
                    NewsFK=4
                },
                new Photos {
                    ID=5,
                    Name="News5.jpg",
                    NewsFK=5
                },
                new Photos {
                    ID=6,
                    Name="News6.jpg",
                    NewsFK=6
                },
                new Photos {
                    ID=7,
                    Name="News7.jpg",
                    NewsFK=7
                },
                new Photos {
                    ID=8,
                    Name="News8.jpg",
                    NewsFK=8
                },
                new Photos {
                    ID=9,
                    Name="News9.jpg",
                    NewsFK=9
                },
                new Photos {
                    ID=10,
                    Name="News10.jpg",
                    NewsFK=10
                },
                new Photos {
                    ID=11,
                    Name="News11.jpg",
                    NewsFK=11
                },
                new Photos {
                    ID=12,
                    Name="News12.jpg",
                    NewsFK=12
                },
                new Photos {
                    ID=13,
                    Name="News13.jpg",
                    NewsFK=13
                },
                new Photos {
                    ID=14,
                    Name="News14.jpg",
                    NewsFK=14
                },
                new Photos {
                    ID=15,
                    Name="News15.jpg",
                    NewsFK=15
                },
                new Photos {
                    ID=16,
                    Name="News16.jpg",
                    NewsFK=16
                },
                new Photos {
                    ID=17,
                    Name="News17.jpg",
                    NewsFK=17
                },
                new Photos {
                    ID=18,
                    Name="News18.jpg",
                    NewsFK=18
                },
                new Photos {
                    ID=19,
                    Name="News19.jpg",
                    NewsFK=18
                },
                new Photos {
                    ID=20,
                    Name="News20.jpg",
                    NewsFK=18
                },
                new Photos {
                    ID=21,
                    Name="News21.jpg",
                    NewsFK=18
                },
                new Photos {
                    ID=22,
                    Name="News22.jpg",
                    NewsFK=19
                },
                new Photos {
                    ID=23,
                    Name="News23.jpg",
                    NewsFK=20
                },
                new Photos {
                    ID=24,
                    Name="News24.jpg",
                    NewsFK=20
                }
            };
            photos.ForEach(pp => context.Photos.AddOrUpdate(p => p.ID, pp));
            context.SaveChanges();




        }
    }
}
