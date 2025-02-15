namespace DalTest;
using DalApi;
using DO;
using System;

public static class Initialization
{
    private static IDal? s_dal;

    private static readonly Random s_rand = new();

    public static void Do()
    {
        s_dal = DalApi.Factory.Get;

        createDependence();
        createTask();
        createWorker();
    }

    //public static void Do(IWorker? dalWorker, ITask? dalTask, IDependence? dalDependence)
    //{
    //    s_dalWorker = dalWorker   ?? throw new NullReferenceException("DAL can not be null!");
    //    s_dalTask = dalTask ?? throw new NullReferenceException("DAL can not be null!");
    //    s_dalDependence = dalDependence ?? throw new NullReferenceException("DAL can not be null!");

    //    //calling functions tp initialize the entities
    //    createDependence();
    //    createTask();   
    //    createWorker(); 
    //}

    /// <summary>
    /// initialize Worker data
    /// </summary>
    private static void createWorker()
    {
        string[] workerNames =
        {
        "Dani Levi", "Eli Amar", "Yair Cohen",
        "Ariela Levin", "Dina Klein", "Shira Israelof"
        };

        string[] workerGmails =
        {
        "DaniLevi@gmail.com", "EliAmar@gmail.com", "YairCohen@gmail.com",
        "ArielaLevin@gmail.com", "DinaKlein@gmail.com", "ShiraIsraelof@gmail.com"
        };

        int[] _role = { 1, 2, 3, 4, 5, 6 };

        for (int i = 0; i < 6; i++)
        {
            int _id;
            do
                _id = s_rand.Next(200000000, 400000000);
            while (s_dal!.Worker.Read(_id) != null); //check that the rand id doesnt exist aiready, if it exist it rand again

            int _cost = s_rand.Next(150000, 300000);

            Worker newWrk = new(_id, workerNames[i], workerGmails[i], _cost, (/*DO.*/Roles)_role[i]);

            s_dal!.Worker.Create(newWrk);
        }
    }

    /// <summary>
    /// initialize Dependence data
    /// </summary>
    private static void createDependence()
    {
        s_dal!.Dependence.Create(new Dependence(0, 1, 2));
        s_dal!.Dependence.Create(new Dependence(0, 2, 3));
        s_dal!.Dependence.Create(new Dependence(0, 3, 4));
        s_dal!.Dependence.Create(new Dependence(0, 4, 5));

        s_dal!.Dependence.Create(new Dependence(0, 4, 6));
        s_dal!.Dependence.Create(new Dependence(0, 4, 8));

        s_dal!.Dependence.Create(new Dependence(0, 7, 9));

        s_dal!.Dependence.Create(new Dependence(0, 5, 13));
        s_dal!.Dependence.Create(new Dependence(0, 6, 13));
        s_dal!.Dependence.Create(new Dependence(0, 9, 13));

        s_dal!.Dependence.Create(new Dependence(0, 8, 10));
        s_dal!.Dependence.Create(new Dependence(0, 8, 11));
        s_dal!.Dependence.Create(new Dependence(0, 10, 13));
        s_dal!.Dependence.Create(new Dependence(0, 11, 12));
        s_dal!.Dependence.Create(new Dependence(0, 12, 13));
        s_dal!.Dependence.Create(new Dependence(0, 13, 14));
        s_dal!.Dependence.Create(new Dependence(0, 13, 15));
        s_dal!.Dependence.Create(new Dependence(0, 13, 16));
        s_dal!.Dependence.Create(new Dependence(0, 14, 25));
        s_dal!.Dependence.Create(new Dependence(0, 14, 26));
        s_dal!.Dependence.Create(new Dependence(0, 16, 23));
        s_dal!.Dependence.Create(new Dependence(0, 16, 24));
        s_dal!.Dependence.Create(new Dependence(0, 15, 17));
        s_dal!.Dependence.Create(new Dependence(0, 17, 18));
        s_dal!.Dependence.Create(new Dependence(0, 17, 19));
        s_dal!.Dependence.Create(new Dependence(0, 17, 20));
        s_dal!.Dependence.Create(new Dependence(0, 19, 21));
        s_dal!.Dependence.Create(new Dependence(0, 20, 22));
        s_dal!.Dependence.Create(new Dependence(0, 21, 23));
        s_dal!.Dependence.Create(new Dependence(0, 21, 24));
        s_dal!.Dependence.Create(new Dependence(0, 22, 23));
        s_dal!.Dependence.Create(new Dependence(0, 22, 24));
        s_dal!.Dependence.Create(new Dependence(0, 23, 25));
        s_dal!.Dependence.Create(new Dependence(0, 23, 26));
        s_dal!.Dependence.Create(new Dependence(0, 24, 25));
        s_dal!.Dependence.Create(new Dependence(0, 24, 26));
        s_dal!.Dependence.Create(new Dependence(0, 25, 27));
        s_dal!.Dependence.Create(new Dependence(0, 26, 27));
        s_dal!.Dependence.Create(new Dependence(0, 27, 28));
    }

    /// <summary>
    /// initialize Task data
    /// </summary>
    private static void createTask()
    {
        #region Alias
        string[] alias =
        {
            "script writing",//1
            "Short script",//2                 
            "Recruiting investors",//3        
            "Script correction",//4   
            "Hiring makeup artists",//5
            "Production portfolio",//6
            "Auditions",//7
            "Distribution of script",//8
            "casting",//9
            "Finding locations",//10
            "Choosing outfits",//11
            "Buying outfits",//12
            "Shooting scenes",//13
            "Pay salaries to players", //14
            "Narration recording",//15
            "Shooting behind the Scenes",//16
            "Connecting scenes",//17
            "Editing a trailer",//18
            "Video Editing",//19
            "Audio editing",//20
            "Adding effects (visual)",//21
            "Adding effects (audio)",//22
            "Advertising design",//23
            "Advertising design (media)",//24
            "Publication of advertising materials",//25
            "Actors interviews",//26
            "Signing contracts",//27
            "Ticket sales"//28
        };
        #endregion
        #region Description
        string[] description =
        {
            /*1*/"Writing a script: The task includes writing a script for the film, ensuring a well - structured narrative and engaging dialogue.",
            /*2*/"Short script: creating a concise and focused script for a short film, capturing a specific story or concept in a limited running time.",
            /*3*/"Recruiting investors: Attracting potential investors to financially support the film project by presenting a convincing investment proposal and presenting its potential returns.",
            /*4*/"Script correction: review and editing of an existing script for coherence, clarity, correct design and adherence to the desired tone and style.",
            /*5*/"Hiring make - up artists: identifying and hiring skilled make - up artists who can bring the characters to life through make - up and prosthetics.",
            /*6*/"Production Portfolio: Compile a comprehensive portfolio showcasing previous film projects, including visuals, synopses and critical acclaim to impress potential collaborators and investors.",
            /*7*/"Auditions: organizing and conducting casting sessions for auditioning actors for the various roles in the film, evaluating their suitability and talent.",
            /*8*/"Script distribution: distribution of copies of the script to the cast, crew and other relevant parties involved in the production.",
            /*9*/"Casting: choosing and hiring the most suitable actors for the desired roles in the film, based on their auditions and their suitability for the characters.",
            /*10*/"Finding locations: Scouting and securing suitable filming locations that suit the script's requirements and enhance the visual story.",
            /*11*/"Selection of costumes: cooperation with costume designers to select appropriate clothing and accessories that reflect the characters' personalities and the visual style of the film.",
            /*12*/"Buying costumes: the purchase of the costumes and accessories required for the film, while making sure to match the creative vision and budget limitations.",
            /*13*/"Shooting scenes: physical shooting of the planned scenes on the set or location, using cameras, lights and other equipment under the guidance of the photographer and director.",
            /*14*/"Pay salaries to players: Payment to the actors who participated in the film for their participation.",
            /*15*/"Narration recording: recording voiceover voiceovers or other supplementary audio elements separately from the footage of the scene to be integrated during post-production.",
            /*16*/"Behind - the - scenes photography: photography of the production process, including interviews with the actors and crew, to create promotional material or documentary-style content.",
            /*17*/"Connecting scenes: smooth transition between different scenes to maintain continuity and coherence throughout the film, which ensures a smooth flow of the narrative.",
            /*18*/"Editing a trailer: putting together a captivating and engaging short video highlighting key moments, themes and the general tone of the film, with the aim of creating interest and attracting an audience.",
            /*19*/"Video editing: selection and arrangement of the photographs taken, adding visual effects, color grading and general design of the visual story",
            /*20*/"Audio editing: refining and improving the recorded dialogue, sound effects and music using techniques such as noise reduction, accumulation and mixing to achieve optimal audio quality.",
            /*21*/"Adding visual effects: combining computer-generated imagery(CGI), practical effects or other visual enhancements to create impactful and visually stunning moments in a film.",
            /*22*/"Adding audio effects: includes additional sound effects, poly sounds and atmospheric sounds to enhance the overall audio experience and create a more immersive environment.", 
            /*23*/"Advertising design: creating visually appealing promotional materials such as posters, banners and digital assets to market the film and create audience interest.",
            /*24*/"Advertising Design(Media): Strategy and execution of marketing campaigns in various media channels such as television, radio, print, online platforms and social media to maximize exposure and exposure of the film.",
            /*25*/"Publication of advertising materials: After the advertising holes have been designed, they should be published to the general public.",
            /*26*/"Cast Interviews: Conducting interviews with cast members to capture their insights, experiences and perspectives on the film, which can be used for promotional purposes or behind - the - scenes content.",
            /*27*/"Signing contracts: negotiating and formulating legal agreements with actors, crew members and other stakeholders to ensure smooth cooperation and protection of the rights and obligations of all parties involved in the film production process.",
            /*28*/"Ticket sales: time to enjoy our hard work, watch the movie and earn money from thcket sales."
        };
        #endregion
        #region Deliverables
        string[] deliverables =
        {
            "script written",//1
            "The script is shortened",//2
            "Investors have been recruited",//3
            "The script has been fixed",//4
            "Make-up artists were hired",//5
            "Production file assembled",//6
            "Auditions done",//7
            "The script is divided into parts",//8
            "actors were cast",//9
            "Locations found",//10
            "outfits chosen",//11
            "outfits bought",//12
            "scenes were filmed",//13
            "Salaries were paid",//14
            "recorded narration",//15
            "Photographs were taken behind the scenes",//16
            "Composed Scenes",//17
            "Trailer Edited",//18
            "video edited",//19
            "audio edited",//20
            "Visual effects added",//21
            "Added sound effects",//22
            "advertisements were designed",//23
            "Advertisements were designed for the media",//24
            "Materials have been published",//25
            "Players interviewed for articles",//26
            "contracts signed",//27
            "Tickets have been sold"//28
        };
        #endregion
        #region Remarks
        string[] remarks =
        {
             "Out first mission. script must be interesting.",//1
             "No remarks",//2
             "No remarks",//3
             "No remarks",//4
             "No remarks",//5
             "No remarks",//6
             "No remarks",//7
             "No remarks",//8
             "No remarks",//9
             "No remarks",//10
             "Make sure the size is fit perfect to the actors size.",//11
             "Don't forget to check the outfits again on actors body.",//12
             "No remarks",//13
             "No remarks",//14
             "No remarks",//15
             "Has to be funny.",//16
             "No remarks",//17
             "Trailer must be a-m-a-z-i-n-g.",//18
             "No remarks",//19
             "No remarks",//20
             "No remarks",//21
             "No remarks",//22
             "No remarks",//23
             "No remarks",//24 
             "No remarks",//25
             "Needs several interviews",//26
             "After the signing, it is recommended to open champagne :)",//27
             "enjoy watching:)"//28
        };
        #endregion
        int[] complexity = { 2, 2, 3, 2, 3, 3, 1, 1, 1, 1, 1, 3, 4, 3, 6, 4, 5, 5, 5, 5, 4, 6, 5, 5, 3, 3, 3, 3 };

        //random variables
        DateTime s_now = DateTime.Now;
        DateTime s_projectStart = s_now.AddMonths(2);

        for (int i = 0; i < 28; i++)
        {

            int rangeStart = (s_projectStart - s_now).Days;
            DateTime createDate = s_projectStart.AddDays(s_rand.Next(rangeStart));//random date
          
            Task newTsk = new(0, null, false, alias[i], description[i], createDate, null, null, TimeSpan.FromDays(new Random().Next(30, 60)),
                               null, null, deliverables[i], remarks[i], (/*DO.*/Roles)complexity[i]);

            s_dal!.Task.Create(newTsk);
        }
    }
}
