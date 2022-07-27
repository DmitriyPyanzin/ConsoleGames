//Доброго времени суток Денис! Вот такое получилось RPG (Демо версия).
//Во-первых я как руководитель проекта неудался)))) Вроде с ребятами обсудили что делать, а в итоге делали 2 разных игры))) Вот этот мой вариант, а другой с ребятами как доделаем, обязательно вам вышлем, она тоже RPG, но карточная.
//Во-вторых сюжет вроде бы не к чему, но с ним интереснее. В любом случае поменять все довольно просто, т.к. созданы рабочии методы. Как говорится - бери и переставляй
//В-третьих костылей программирования хватает, но насколько хватило знаний))))
//В-четвертых третью главу пока не сообразил как сократить, т.е. как ее засунуть в метод, так что пока так
//Вопросы написаны в самом низу программы
//На этом вроде бы все. Жду вашей рецензии!


//Главный метод

void HEAD()
{
    //Переменные

    string answer = String.Empty;                                       //Переменная для ответов в циклах while do

    bool death = false;                                                 //Переменная жизни

    int chapterCount = 0;                                               //Переменная счетчика глав
    int karma = 0;                                                      //Переменная кармы
    int wallet = 10;                                                    //Переменная кошелек

    //Определение персонажей
    
    //             0   1   2  3  4  5  6
    int[] hero = {100, 20, 1, 1, 0, 0, 0};                            // {0 здоровье, 1 атака, 2 защита, 3 зелья здоровья, 4 удача, 5 особые возможности, 6 магия}

    int[] skeleton = {130, 15, 1, 0, 0};
    int[] zombie = {150, 17, 2, 0, 0};

    //Логика игры

    Console.Clear();
    answer = Greeting(answer);
    
    if (answer == "да")
    {
        History();

        while (true)
        {
            //Первая глава

            ChapterOneBeginning();
            death = Attack(hero, skeleton, chapterCount, karma, death);
            if(death == true) break;
            wallet = Salary(skeleton, wallet);
            ChapterOneEnd(hero);
            hero = ExpHeroChange(hero);
            HeroPow();
            karma = ChangingKarma(karma, chapterCount, hero);
            chapterCount++;

            //Вторая глава

            ChapterTwoBeginning(hero);
            death = Attack(hero, zombie, chapterCount, karma, death);
            if(death == true) break;
            wallet = Salary(zombie, wallet);
            ChapterTwoEnd();
            hero = ExpHeroChange(hero);
            HeroPow();
            KarmicConsequences(hero, chapterCount, karma);
            chapterCount++;

            //Третья глава

            ChapterThreeBeginning();

            while (true)
            {   
                Console.WriteLine("Что ты выберешь?");
                Console.WriteLine("1 - зайти в магазин");
                Console.WriteLine("2 - погулять по кладбищу");
                Console.WriteLine("3 - отправиться к травнику");

                answer = Console.ReadLine();

                if (answer == "1")
                {
                    wallet = Market(hero, wallet, chapterCount);
                    Console.WriteLine(wallet);
                }

                if (answer == "2")
                {
                    while (true)
                    {
                        Console.WriteLine("Подойдя к кладбищу, вы заметили как по кладбищу бродит нежить.");
                        Console.WriteLine("Что герой должен сделать?");
                        Console.WriteLine("1 - Напасть на скелета");
                        Console.WriteLine("2 - Напасть на зомби");
                        Console.WriteLine("0 - Уйти с кладбища");
                        answer = Console.ReadLine();

                        if (answer == "1")
                        {
                            death = Attack(hero, skeleton, chapterCount, karma, death);
                            if (death == true) break;
                            wallet = Salary(skeleton, wallet);
                            hero = ExpHeroChange(hero);
                            HeroPow();
                        }

                        else if (answer == "2")
                        {
                            death = Attack(hero, zombie, chapterCount, karma, death);
                            if (death == true) break;
                            wallet = Salary(zombie, wallet);
                            hero = ExpHeroChange(hero);
                            HeroPow();
                        }
                        
                        else if (answer == "0") break;

                        else Console.Clear();
                    }

                    if (death == true) break;
                }

                else if (answer == "3") break;

                else Console.Clear();
            }

            if (death == true) break;

            karma = ChangingKarma(karma, chapterCount, hero);

            Console.WriteLine("Для продолжения игры вам необходимо приобрести абонимент за 5000 рублей! Шутка! Спасибо, что поиграли в игру!");
            Console.WriteLine("Я срочно смотреть лекции и делать домашки, а то набрал очень много хвостов. Как продолжу сообщу!");

            break;
        }

        Final(chapterCount);
        Console.WriteLine("\t " + "\t " + "GAME OVER");
        Console.WriteLine();
    }

    else
    {
        Console.WriteLine("\t " + "\t " + "Серость дней ждет тебя! 'Злобный смех'!");
        Console.WriteLine();
    }
}

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


//Текстовые методы

string Greeting(string answer)                                                                                                          //Метод введения в игру                                 ++
{
    int whileNum;                                                                           //Переменная для выхода из цикла

    Console.WriteLine("Заблудшая душа! Ты стоишь на пороге врат в удивительный и волшебный мир, полный магии и чудес, подлости и отваги. И конечно же извечной борьбы добра со злом!");
    Console.WriteLine();
    Console.WriteLine("Достаточно ли ты смел для такого приключения?");
    Console.WriteLine();

    do                                                                                                                                      //Цикл защита от дурака
    {
        Console.Write("Скажешь 'ДА' и узришь волшебство, а если скажешь 'НЕТ', то будешь жить дальше в своем сером и скучном мире! ");
        answer = Console.ReadLine().ToLower();
        whileNum = answer == "да" || answer == "нет"? 1:0;
        if (whileNum != 1) Console.Clear();
    }
    while(whileNum != 1);

    Console.WriteLine();

    return answer;
}

void History()                                                                                                                          //Метод История героя                                   ++
{
    int whileNum;                                                                           //Переменная для цикла
    string answer;                                                                          //Переменная для ответа

    Console.WriteLine("Ты решился, я удивлен!");
    Console.WriteLine();

    do                                                                                      //Цикл защита от дурака
    {
        Console.Write("Хочешь узнать историю нашего героя? Скажи 'ДА' или 'НЕТ'! ");
        answer = Console.ReadLine().ToLower();                                                                                                      
        whileNum = answer == "да" || answer == "нет"? 1:0;
        if (whileNum != 1) Console.Clear();
    }
    while(whileNum != 1);

    if (answer == "да")
    {
        Console.WriteLine();
        Console.WriteLine("                    ПРОЛОГ");
        Console.WriteLine();
        Console.WriteLine("Война двух королевств затянулась, и к военной службе стали привлекать всякое отребье: крестьян, свинопасов и прочий сброд, в число которых и входил  наш герой.");
        Console.WriteLine("Героя нашего звали Феофил. Да странное имя, но его родителям нравилось. При этом мальчик страдал всю жизнь от подколок по поводу своего имени, и особенно от трех последних букв 'фил'.");
        Console.WriteLine("В боях он не участвовал, так как со знаменем в руках это практически невозможно, но внимательно наблюдал за воинами и мотал на ус технику владения оружием.");
        Console.WriteLine("После дембеля наш герой получил 10 монет и пинок под зад. И вот он возвращается в родное село.");
        Console.WriteLine();
        Console.WriteLine("Для продолжения нажмите клавишу 'Enter'");
        Console.ReadLine();
    }

    else Console.WriteLine();
}

void HeroPow()                                                                                                                          //Метод сообщение о восстановлении сил                  ++
{
    Console.WriteLine("Силы героя восстановились и он стал сильнее");
    Console.WriteLine();
    Console.WriteLine("Для продолжения нажмите клавишу 'Enter'");
    Console.ReadLine();
}

void Final(int chapterCount)                                                                                                            //Метод финала                                          ++
{   
    if (chapterCount == 0) Console.WriteLine("Слабовато! Сначала пройди все три части Dark Souls и только после этого возвращайся!");

    else if (chapterCount == 1) Console.WriteLine("Тебя замочила твоя бабуля-зомби!!! ХА-ХА-ХА!!!");

    else if (chapterCount == 2) Console.WriteLine("Это все, что ты смог из себя выжать? Мдаааа...");

    Console.WriteLine();
}

void Signature()                                                                                                                        //Метод создатели игры                                  ++
{    
    Console.WriteLine();
    Console.WriteLine("________________________");
    Console.WriteLine("Идейный вдохноваитель Денис Сапрыкин");
    Console.WriteLine("Создание игры Дмитрий Пьянзин");
    Console.WriteLine("________________________");
    Console.WriteLine();
}

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


//Методы вывода описания глав

void ChapterOneBeginning()                                                                                                              //Метод описания начала главы 1                         +
{
    Console.WriteLine("                    ГЛАВА I. На задворках родного дома");
    Console.WriteLine();
    Console.WriteLine("Наш герой не спеша брел по пустынной дороге. Погода была холодной, пасмурной и дождливой, а туман такой, что не видно дальше собственного носа. Но герой знал, что он уже близок к дому.");
    Console.WriteLine("Несмотря на ненастную погоду, шагах в десяти от себя он увидел человеческий силует, стоящий в неестественной и какой-то скрюченной позе. Подойдя к нему, наш герой остолбенел! Это был живой скелет, еще с остатками гнилой полоти.");
    Console.WriteLine("Мертвяк увидел нашего героя и двинулся на него, в глазах горела злоба и явное желание убивать, прям как у армейского старшины. В этот момент Феофил попятился назад и споткнувшись рухнул на землю.");
    Console.WriteLine("Прейдя в себя, он увидел, что споткнулся о мертвое и уже изрядно изувеченное тело. Из его живота торчал меч, а в руке застыло зелье исцеления, точно такое же выдавали в армейке. Феофил схватил меч и зелье и приготовился к бою.");
    Console.WriteLine();
    Console.WriteLine("Для продолжения нажмите клавишу 'Enter'");
    Console.ReadLine();
}

int[] ChapterOneEnd(int[] hero)                                                                                                         //Метод описания конца главы 1                          ++
{
    Console.WriteLine("Отдышавшись, наш герой пытался понять то, что произошло. Откуда в его родном селе подобные твари, о которых он слышал только с пьяных рассказов явно не очень здоровых на голову людей!");
    Console.WriteLine("Также было непонятно, кому принадлежал обезображенный труп. Рядом с ним лежала дорожная сумка. Феофил решил проверить сумку и очень обрадовался еще двум бутылочкам зелья здоровья.");
    Console.WriteLine("В сумке также были такие книги: 'Как знак ИГНИ сделал мою жизнь веселей', 'Волшебная палочка Гарри и Гермиона', 'Как приготовить глазные капли для Саурона'. А вот! ");
    Console.WriteLine("'Прикладная боевая магия для чайников', явно полезная книга. Теперь пригодится умение читать, которому его научил местный травник еще в детстве. После этого герой направился в сторону дома!");
    Console.WriteLine();
    Console.WriteLine("Теперь вам доступна книга заклинаний. И первое заклинание 'Огненный шар'");
    Console.WriteLine();
    Console.WriteLine("Для продолжения нажмите клавишу 'Enter'");
    Console.ReadLine();

    hero[6]++;
    hero[3] += 2;

    return hero;
}

int[] ChapterTwoBeginning(int[] hero)                                                                                                   //Метод описания начала главы 2                         ++
{
    Console.WriteLine("                    ГЛАВА II. Дом! Милый дом.");
    Console.WriteLine();
    Console.WriteLine("Войдя в село, наш герой сразу почувствовал себя лучше. Куры, овцы, коровы - все это навевало приятные воспоминания. Но было и то, что вызывало беспокойство. Во-первых все вокруг было каким-то запустевшим!");
    Console.WriteLine("Во-вторых людей в округе было непривычно мало, да и те выглядели напуганными с обезумевшими глазами . Кто-то из горожан сказал, что лучьше б Феофил погиб на той войне! Всё это было очень странно!");
    Console.WriteLine("А самое главное в воздухе ощущалось зло! Не то зло, которое творят люди, наподобии справления нужды в подъезде, пьяных драк или горцевание на лошади перед открытым окном в летнюю ночь. А настоящее зло!");
    Console.WriteLine();
    Console.WriteLine("Прийдя домой герой рад был видеть своих постаревших родителей и младшего братика. Обнимались, плакали и много говорили! Феофил не стал рассказывать о встречи с монстром, а когда сели обедать, из погреба донесся жуткий грохот!");
    Console.WriteLine("'Хватить бабуля, ты напугаешь Феофила!' крикнул брат! А наш герой застыл с куском хлеба во рту, так как бабушка умерла два года назад перед проводами. Он молча встал, взял меч и пошел в сторону погреба!");
    Console.WriteLine("По пути наш герой заметил висящий на стене арбалет Калашникова. Он был именной, подаренный отцу обществом ветеранов МВД за безупречную и долгую службу районым участковым. Пригодится, подумал он и, сняв его со стены, спустился в погреб!");
    Console.WriteLine("Перекошенное лицо, глаза смотрят в разные стороны, торчат зубы, синюшная кожа, и если бы не жуткая вонь, то можно было бы сказать, что она живая, хотя пахло от нее чуть похуже, чем при жизни! Она сказала приветливо 'Мозгиии!' и двинуласьв в сторону героя!");
    Console.WriteLine("И Феофил тяжело вздохнув сказал: 'Знаешь бабуль, несмотря на то, что твои пирожки всегда были на вкус, как собачье дерьмо, я все равно люблю тебя и избавлю тебя от страданий', затем он взял в руки арбалет и пошел убивать зомби-старушку!");
    Console.WriteLine();
    Console.WriteLine("Поздравляем! теперь у вас есть арбалет, и вы можете нанести урон врагу до начала боя");
    Console.WriteLine();
    Console.WriteLine("Для продолжения нажмите клавишу 'Enter'");
    Console.ReadLine();

    hero[5]++;

    return hero;
}

void ChapterTwoEnd()                                                                                                                    //Метод описания конца главы 2                          ++
{
    Console.WriteLine("После вторых похорон бабульки, у героя возникло множество вопросов, ведь встретить двух ходячих мертвяков за день это явный перебор. И родители рассказали о всех невзгодах, которые свалились на местных жителей.");
    Console.WriteLine("Оказывается среди мужчин только он лошара неоткосил от армии, все остальные изобразили слепоту, плоскостопие и прочие недуги, нашлись даже те кто прикинулся любителем других мужчин!");
    Console.WriteLine("Но счастье их длилось не долго. Сначала стали пропадать люди, затем появились слухи о ходячих мертвецах (в это время как раз вернулась бабуля). Стали находить обескровленных или разорванных на куски животных!");
    Console.WriteLine("Горожане отправили письмо, с просьбой о помощи в церковь, но оттуда пришел такой ответ: 'Молитесь больше. С вас 1000 золотых монет. P.S. Покупайте свечи только у нас, в других местах подделка. Оптом дешевле.'");
    Console.WriteLine("Но это было не все. Запретил продажу пива по ночам, мат в общественных местах, мультик 'Ну погоди!' из-за того что Волк курит! Заставили носить всем намордники и стоять не ближе, чем 1,5 метра и еще много чего.");
    Console.WriteLine("И охватило людей чувство страха, безысходности и отчаяния. Вокруг одна дипрессия и уныние, и никакой уверенности в завтрашнем дне. И тут герой понял, что теперь только он может спасти родной дом от зла!");
    Console.WriteLine();
    Console.WriteLine("Для продолжения нажмите клавишу 'Enter'");
    Console.ReadLine();
}

void ChapterThreeBeginning()                                                                                                            //Метод описания начала главы 3                         +++
{
    Console.WriteLine("                    ГЛАВА III. На отшибе у траника");
    Console.WriteLine();
    Console.WriteLine("Энтузиазма у героя было хоть отбавляй, но куда идти дальше было непонятно. И вдруг он вспомнил про травника! Этот мужик был умный и наверняка знал, что за СЮР вокруг происходит!");
    Console.WriteLine("Но перед этим неплохо было бы прикупить снаряжения и торговая лавка была как раз по пути, напротив сгоревшей церкви, от которой остались только обугленные стены");
    Console.WriteLine("По пути наш герой встретил пьяного мужика, явно похожего на кота, который говорил о теплых песках какого-то Эльсвейра. Еще какие-то, в странных костюмах, сидели у костра, играли на гитаре и травили анекдоты!");
    Console.WriteLine("Подойдя к магазину он увидел табличку, на которой написано 'В продаже есть снаряжения и зелья, еда и напитки, а так же' и слово 'Coca-Cola' зачеркнуто, и подписано слово 'Байкал'.");
    Console.WriteLine("С другой стороны на обугленной стене большими буквами надпись 'Цой Жив!', и еще много непонятных граффити. А под стеной было огромное, в человеческий рост, разбитое яйцо!");
    Console.WriteLine("И вдруг героя осенило! За церковью же кладбище, оттуда как раз и пришла бабуля. Надо бы тоже сходить проверить, вдруг там тоже обосновалось зло!");
    Console.WriteLine();
    Console.WriteLine("Для продолжения нажмите клавишу 'Enter'");
    Console.ReadLine();
}

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


//Методы окончания боя

void Victory(int[] enemy)                                                                                                               //Метод победы          ++
{
    Console.WriteLine();

    if(enemy[0] == 130)                                                         //Победа над скелетом
    {
        Console.WriteLine("После точного удара по черепушке, скелет рассыпался на множество мелких костей!");
        Console.WriteLine("Осталось надеяться, что он упокоился и больше никогда не восстанет!");
    }

    else if (enemy[0] == 150)                                                   //Победа над зомби
    {
        Console.WriteLine("Блик лезвия меча и тупая бошка зомби отделилась от тела!");
        Console.WriteLine("Гниющий вонючий труп, подняв столб пыли, рухнул на землю!");
    }

    Console.WriteLine();
    Console.WriteLine("Для продолжения нажмите клавишу 'Enter'");
    Console.ReadLine();
}

void Death(int[] enemy)                                                                                                                 //Метод смерти          ++
{
    Console.WriteLine();

    if(enemy[0] == 130)                                                                             //смерть от скелета
    {
        Console.WriteLine("Герой не успел оглянуться, как скелет впился в его шею и перегрыз ему сонную артерию!");
        Console.WriteLine("Феофил еще видел и чувствовал как мертвяк вырывал его внутренности!");
        Console.WriteLine("Через какое-то время наш герой восстал, неся смерть всем живым, встретившимся ему на пути!");
    }

    else if (enemy[0] == 150)                                                                       //Смерть от зомби
    {
        Console.WriteLine("Герой оступился и упал не землю. В этот момент зомби навалился на него, и прокусив череп стал выедать его мозг!");
        Console.WriteLine("Конечно же Феофил превратился в вонючего и тупого зомби, и единственной целью его было поедание чужих мозгов!");
        Console.WriteLine("Однажды молния ударила в сухую сосну и в лесу начался пожар, в котором и сгорел наш герой! Нехватило мозга убежать!!!");

    }

    Console.WriteLine();
    Console.WriteLine("Для продолжения нажмите клавишу 'Enter'");
    Console.ReadLine();
}

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


//Методы вывода персонажей

void PrintEnemy(int[] tempEnemyArray)                                                                                                   //Метод вывода характеристик врага             +
{
    Console.WriteLine("         ХАРАКТЕРИСТИКИ ВРАГА");
    Console.WriteLine();
    Console.WriteLine("Здоровье       Атака         Защита");
    for (int i = 0; i < tempEnemyArray.Length - 2; i++) Console.Write(tempEnemyArray[i] + "\t" + "\t");
    Console.WriteLine();
    Console.WriteLine();
}

void PrintHero(int[] tempHeroArray)                                                                                                     //Метод вывода характеристик героя             +
{
    Console.WriteLine("         ХАРАКТЕРИСТИКИ ГЕРОЯ");
    Console.WriteLine();
    Console.WriteLine("Здоровье       Атака         Защита      Зелья здоровья");
    for (int i = 0; i < tempHeroArray.Length - 3; i++) Console.Write(tempHeroArray[i] + "\t" + "\t");
    Console.WriteLine();
    Console.WriteLine();
}

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


//Крики во время боя

void CryOfJubilation(int karma)                                                                                                         //Метод атакующих криков                        +++
{
    Random rand = new Random();
    int index = rand.Next(0, 5);

    Console.WriteLine();

    if (karma > -3 && karma < 3) 
    {
        string[] cry0 = {"За моего отца!", "Сдохни, порождение смерти!", "А вы ничему не учитесь, да?", "Отведай моего меча!", "За Альянс!"};
        Console.WriteLine(cry0[index]);
    }

    else if (karma < -2)
    {
        string[] cry1 = {"Почуствуй мощь темной стороны", "Я с превиликим удовольствие убью тебя второй раз", "Такая власть и не снилась моему отцу!", "Даже если ты сдашься, я все равно убью тебя!", "Фростморн жаждет крови!"};
        Console.WriteLine(cry1[index]);
    }

    else if (karma > 2)
    {
        string[] cry2 = {"Я направлю тебя к свету брат! Или ты сестра?", "Я очищу этот мир от зла!", "Я упокою тебя с миром!", "За небесное воинство!", "За честь и отвагу!"};
        Console.WriteLine(cry2[index]);
    }

    Console.WriteLine();
    Console.WriteLine("Для продолжения нажмите клавишу 'Enter'");                                    
    Console.ReadLine();  
}

void ScreamOfPain()                                                                                                                     //Метод криков боли                             +++
{
    Random rand = new Random();
    int index = rand.Next(0, 5);
    string[] scream = {"Мама, мне больно!", "И все! Моя бабуля била сильнее!", "И это по твоему удар! Вот удар!", "Я отомщу! Обязательно отомщу!", "Зря ты так поступил! Теперь ходи и оборачивайся"};

    Console.WriteLine();
    Console.WriteLine(scream[index]);
    Console.WriteLine();
    Console.WriteLine("Для продолжения нажмите клавишу 'Enter'");
    Console.ReadLine();
}

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


//Боевые методы

bool WhoAttackFirst(bool flag)                                                                                                          //Метод первого атакующего                             ++
{
    Random rand = new Random();
    int num = rand.Next(0, 2);
    
    string text = num == 1? "Герой ходит первый":"Противник ходит первый";
    flag = num == 1? true:false;
    Console.WriteLine(text);

    Console.WriteLine();
    Console.WriteLine("Для продолжения нажмите клавишу 'Enter'");
    Console.ReadLine();

    return flag;
}

bool Attack(int[] hero, int[] enemy,  int chapterCount, int karma, bool death)                                                          //Метод боя                                            +++++++
{
    int[] tempHeroArray = new int[hero.Length];                                                                 //Временный массив героя для боя
    int[] tempEnemyArray = new int[enemy.Length];                                                               //Временный массив врага для боя

    string answer = String.Empty;                                                                               //Переменная для ответов
    int magicStop = 0;                                                                                          //Переменная блокировки магии

    FillArray(hero, tempHeroArray);                                                                             //Заполняем временный массив героя
    FillArray(enemy, tempEnemyArray);                                                                           //Заполняем временный массив врага
    
    ExpHero(tempHeroArray, chapterCount);                                                                       //Прокачиваем героя
    ExpEnemy(tempEnemyArray, chapterCount);                                                                     //Прокачиваем врага

    int health = tempHeroArray[0];                                                                              //Переменная для лечения
    
    if (hero[5] > 0)                                                                                            //Применение спец возможностей перед боем
    {
        answer = SpecialFoolProof(tempHeroArray, answer);                                                       //Применение защиты от дурака

        if (answer == "1" || answer == "7") tempEnemyArray = Shot(tempEnemyArray, answer);                      //Применение дальней атаки

        else tempHeroArray = SpecialFeatures(tempHeroArray, answer);                                            //Применение замены оружия
    }

    bool flagAttack = true;                                                                                     //Переменная для очередности атаки
    flagAttack = WhoAttackFirst(flagAttack);                                                                    //Вызов метода кто превый атакует

    while (true)
    {
        if (tempEnemyArray[0] <= 0)                                                                              //Победа над врагом
        {
            Victory(enemy);                                                                                     
            death = false;                                                                                      
            break;
        }

        if(tempHeroArray[0] <= 0)                                                                           //Смерть героя
        {
            Death(enemy);
            death = true;
            break;
        }

        if (magicStop > 0) magicStop--;                                                                         //Время восстановления маны

        if (flagAttack == true)                                                                                 //Атака героя
        {
            answer = AttackFoolProof(tempHeroArray, tempEnemyArray, health, answer, magicStop, chapterCount);   //Защита от дурака на атаку

            if (answer == "3")                                                                                  //Вызов метода лечения
            {
                hero[3] -= 1;
                EnergyPotion(tempHeroArray, health);
            }

            else if (answer == "1")                                                                             //Вызов метода атаки
            {
                Damage(tempHeroArray, tempEnemyArray);                                                          //Вызов метода урона
                CryOfJubilation(karma);
            }

            else if (answer == "2")
            {
                answer = MagicFoolProof(karma, chapterCount, answer);
                if (answer == "2" || answer == "3") tempHeroArray = MagicToHero(tempHeroArray, answer, karma);  //Вызов метода магии на героя
                else tempEnemyArray = MagicToEnemy(tempEnemyArray, answer, karma);                              //Вызов метода магии на врага
                magicStop = 3;
            }

            else if (answer == "scorpion")                                                                      //Призыв скорпиона
            {
                tempEnemyArray[0] = 0;

                Console.WriteLine();
                Console.WriteLine("Вы призвали скорпиона, и последнее, что услышал враг это get over here!");
                Console.WriteLine("Апперкот, вертушка, комбо и конечно же FATALLITY! Scorpion Wins!");
                Console.WriteLine();
                Console.WriteLine("                            FLAWLESS VICTORY!");
                Console.WriteLine();
                Console.WriteLine("Для продолжения нажмите клавишу 'Enter'");
                Console.ReadLine();
            }

            flagAttack = false;
        }

        else if(flagAttack == false)                                                                            //Атака врага
        {
            Console.WriteLine("Ход противника!");
            EnemyAttack(enemy, tempHeroArray, tempEnemyArray);
            ScreamOfPain();

            flagAttack = true;
        }
    }

    return death;
}

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


//Методы во время боя

string AttackFoolProof(int[] tempHeroArray, int[] tempEnemyArray, int health, string answer, int magicStop, int chapterCount)           //Метод защиты от дурака для атаки              +++++
{
    int whileNum;
    answer = String.Empty;

    do
    {
        PrintHero(tempHeroArray);
        PrintEnemy(tempEnemyArray);

        Console.WriteLine("Для выбора действия нажмите цифру:");
        Console.WriteLine("'1' - Атаковать");
        if (tempHeroArray[6] > 0 && magicStop == 0) Console.WriteLine("'2' - Применить заклинание");
        if (tempHeroArray[3] > 0 && tempHeroArray[0] != health) Console.WriteLine("'3' - Выпить зелье здоровья");
        if (chapterCount > 2) Console.WriteLine("'0' - Убежать, спрятаться, и плакать");

        answer = Console.ReadLine();
        whileNum = answer == "1" || answer == "2" || answer == "3" || answer == "0" || answer == "scorpion"? 1:0;
        if (answer == "2" && magicStop != 0 || answer == "2" && tempHeroArray[6] == 0) whileNum = 0;
        if (answer == "3" && tempHeroArray[0] == health || answer == "3" && tempHeroArray[3] == 0) whileNum = 0;
        if (chapterCount < 3 && answer == "0") whileNum = 0;
        if (whileNum == 0) Console.Clear();
    }
    while(whileNum != 1);

    return answer;
}

int[] EnergyPotion(int[] tempHeroArray, int health)                                                                                     //Метод лечения                                 +
{
    tempHeroArray[0] += (health * 50) / 100;
    if (tempHeroArray[0] > health) tempHeroArray[0] = health;
    tempHeroArray[3]--;

    Console.WriteLine();
    Console.WriteLine($"Восстановлено +{(health * 50) / 100} единицы здоровья");
    Console.WriteLine();
    Console.WriteLine("Для продолжения нажмите клавишу 'Enter'");
    Console.ReadLine();

    return tempHeroArray;
}

int[] Damage(int[] arr1, int[] arr2)                                                                                                    //Метод урона                                   ++++
{
    Random damage = new Random();
    int combatMulti = damage.Next(0, 11);

    Console.WriteLine();

    if (arr1[4] == 1) combatMulti++;

    if(combatMulti == 0)
    {
        arr2[0] -= arr1[1] * 50 / 100 + arr2[2];

        Console.WriteLine($"Нанесен минимальный урон {arr1[1] * 50 / 100 + arr2[2]}");
    }

    else if (combatMulti > 0 && combatMulti < 4)
    {
        arr2[0] -= arr1[1] * 75 / 100 + arr2[2];

        Console.WriteLine($"Нанесен урон ниже седнего {arr1[1] * 75 / 100 + arr2[2]}");
    }

    else if (combatMulti > 3 && combatMulti < 7)
    {
        arr2[0] -= arr1[1] + arr2[2];

        Console.WriteLine($"Нанесен средний урон {arr1[1] + arr2[2]}");
    }

    else if (combatMulti > 6 && combatMulti < 10)
    {
        arr2[0] -= arr1[1] * 125 / 100 + arr2[2];

        Console.WriteLine($"Нанесен урон выше среднего {arr1[1] * 125 / 100 + arr2[2]}");
    }

    else if (combatMulti > 9)
    {
        arr2[0] -= arr1[1] * 150 / 100 + arr2[2];

        Console.WriteLine($"Нанесен критический урон {arr1[1] * 150 / 100 + arr2[2]}");
    }

    return arr2;
}

int[] EnemyAttack(int[] enemy, int[] tempHeroArray, int[] tempEnemyArray)                                                               //Метод атаки врага                             ++++
{
    Random damage = new Random();
    int combatMulti = damage.Next(0, 11);

    Damage(tempEnemyArray, tempHeroArray);

    if (tempHeroArray[4] == 1) combatMulti--;

    if (combatMulti > 8)
    {
        if (enemy[0] == 130)
        {
            tempHeroArray[1] -= 2;

            Console.WriteLine();
            Console.WriteLine("Скелет наложил на вас проклятие! Ваша атака уменьшина на -2");
        }

        else if (enemy[0] == 150)
        {
            tempHeroArray[2] -= 2;

            Console.WriteLine();
            Console.WriteLine("Зомби заразил вас чумой! Ваша защита уменьшина на -2");
        }
    }

    return tempHeroArray;
}

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


//Методы специальной атаки

string SpecialFoolProof(int[] tempHeroArray, string answer)                                                                             //Метод защиты от дурака для специальной атаки      ++++
{
    int whileNum;
    answer = String.Empty;

    do
    {
        Console.WriteLine("Для применения дополнительных возможностей нажмите цифру:");
        Console.WriteLine("'1' - Выстрелить из арбалета");
        if (tempHeroArray[5] > 1) Console.WriteLine("'2' - Использовать в бою щит");
        if (tempHeroArray[5] > 2) Console.WriteLine("'3' - Использовать боевую секиру");
        Console.WriteLine("'0' - Не применять специальных возможностей");
        answer = Console.ReadLine();
        whileNum = answer == "1" || answer == "2" || answer == "3" || answer == "7" || answer == "0"? 1:0;
        if (answer == "2" && tempHeroArray[5] < 2) whileNum = 0;
        if (answer == "3" && tempHeroArray[5] < 3) whileNum = 0;
        if (whileNum == 0) Console.Clear();
    }
    while(whileNum != 1);

    return answer;
}

int[] Shot(int[] tempEnemyArray, string answer)                                                                                         //Метод дальней атаки                               ++
{
    Console.WriteLine();

    if (answer == "1")                                                                          //Выстрел с арбалета
    {
        tempEnemyArray[0] -= tempEnemyArray[0] * 10 / 100;                                     

        Console.WriteLine("Лови маслину!!!");
        Console.WriteLine("Противник получил урон 10%");
    }
    
    else
    {
        tempEnemyArray[0] = 0;                                                                  //Выстрел из BFG 9000

        Console.WriteLine("Противник превратился в кучу зеленой жижи.");
        Console.WriteLine("Ничто на свете не вызовет такой улыбки у человека, как примененние BFG 9000");
    }

    Console.WriteLine();
    Console.WriteLine("Для продолжения нажмите клавишу 'Enter'");
    Console.ReadLine();

    return tempEnemyArray;
}

int[] SpecialFeatures(int[] tempHeroArray, string answer)                                                                               //Метод смены оружия                                +++
{
    Console.WriteLine();

    if (answer == "2")                                                                                  //Применить щит
    {
        tempHeroArray[1] -= 5;
        tempHeroArray[2] += 6;

        Console.WriteLine("Вы увеличили защиту на +6, при этом ваша атака уменьшилась на -5");
    }

    else if (answer == "3")                                                                             //Применить секиру
    {
        tempHeroArray[1] += 10;
        tempHeroArray[2] -= 8;

        Console.WriteLine("Вы увеличили атаку на +10, при этом ваша защита уменьшилась на -8");
    }

    else if (answer == "0") Console.WriteLine("Вы предпочли ничего не менять, и сражаться как есть");  //Ничего не применять

    Console.WriteLine();
    Console.WriteLine("Для продолжения нажмите клавишу 'Enter'");
    Console.ReadLine();

    return tempHeroArray;
}

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


//Методы связанные с магией

string MagicFoolProof(int karma, int chapterCount, string answer)                                                                       //Метод защиты от дурака магической книги           +++
{
    int whileNum;
    answer = String.Empty;

    do
    {
        Console.WriteLine("Выберите заклинание");
        Console.WriteLine("'1' - Огненный шар");
        if (chapterCount > 2)
        {
            if (karma < 3 && karma > -3) Console.WriteLine("'2' - Магический щит");
            else if (karma < -2) Console.WriteLine("'2' - Дьявольский щит");
            else if (karma > 2) Console.WriteLine("'2' - Божественный щит");
        } 
        if (chapterCount > 3)
        {
            if (karma < 3 && karma > -3) Console.WriteLine("'3' - Магический меч");
            else if (karma < -2) Console.WriteLine("'3' - Огненный меч");
            else if (karma > 2) Console.WriteLine("'3' - Божественный меч");
        }
        if (chapterCount > 4)
        {
            if (karma < -2) Console.WriteLine("'4' - Призвать армию мертвых");
            else if (karma > 2) Console.WriteLine("'4' - Призвать ангельское войско");
        }
        answer = Console.ReadLine();
        whileNum = answer == "1" || answer == "2" || answer == "3" || answer == "4" || answer == "авада кедавра"? 1:0;
        if (answer == "2" && chapterCount < 3) whileNum = 0;
        if (answer == "3" && chapterCount < 4) whileNum = 0;
        if (answer == "4" && chapterCount < 5 && karma < 3 && karma > -3) whileNum = 0;
        if (whileNum == 0) Console.Clear();
    }
    while(whileNum != 1);

    return answer;
}

int[] MagicToHero(int[] tempHeroArray, string answer, int karma)                                                                        //Метод заклинаний на героя                         +++
{

    Console.WriteLine();

    if (answer == "2" && karma < 3 && karma > -3)
    {
        tempHeroArray[2] += 3;

        Console.WriteLine("Вы преминили заклинание Магический щит. Ваша защита увеличена на +3");
    }

    else if (answer == "2" && karma < -2 )
    {
        tempHeroArray[2] += 6;

        Console.WriteLine("Вы преминили заклинание Дьявольский щит. Ваша защита увеличена на +6");
    }

    else if (answer == "2" && karma > 2 )
    {
        tempHeroArray[2] += 6;

        Console.WriteLine("Вы преминили заклинание Божественный щит. Ваша защита увеличена на +6");
    }

    else if (answer == "3" && karma < 3 && karma > -3)
    {
        tempHeroArray[1] += 4;

        Console.WriteLine("Вы преминили заклинание Магический меч. Ваша атака увеличена на +4");
    }

    else if (answer == "3" && karma < -2 )
    {
        tempHeroArray[1] += 8;

        Console.WriteLine("Вы преминили заклинание Огненный меч. Ваша атака увеличена на +8");
    }

    else if (answer == "3" && karma > 2 )
    {
        tempHeroArray[1] += 8;

        Console.WriteLine("Вы преминили заклинание Божественный меч. Ваша атака увеличена на +8");
    }

    Console.WriteLine();
    Console.WriteLine("Для продолжения нажмите клавишу 'Enter'");
    Console.ReadLine();

    return tempHeroArray;
}

int[] MagicToEnemy(int[] tempEnemyArray, string answer, int karma)                                                                      //Метод заклинаний на врага                         ++++
{

    Console.WriteLine();

    if (answer == "1")
    {
        tempEnemyArray[0] -= tempEnemyArray[0] * 20 / 100;

        Console.WriteLine("Вы нанесли урон врагу огненным шаром");
    }

    else if (answer == "4" && karma < -2)
    {
        tempEnemyArray[0] -= tempEnemyArray[0] * 30 / 100;

        Console.WriteLine("Вы призвали души всех тех, кого вы уничтожили и заставили атаковать врага");
    }

    else if (answer == "4" && karma > 2)
    {
        tempEnemyArray[0] -= tempEnemyArray[0] * 30 / 100;

        Console.WriteLine("Вы призвали ангелов, которые атаковали вашего врага");
    }

    else if (answer == "авада кедавра")
    {
        tempEnemyArray[0] = 0;

        Console.WriteLine("Старое доброе заклинание смерти. Волондеморт одобряет!");
    }

    Console.WriteLine();
    Console.WriteLine("Для продолжения нажмите клавишу 'Enter'");
    Console.ReadLine();

    return tempEnemyArray;
}

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


//Методы по заполнению и прокачке

int[] FillArray(int[] array, int[] temp)                                                                                                //Метод заполнения динамичного массива          +
{
    for (int i = 0; i < array.Length; i++)
    {
        temp[i] = array[i];
    }
    return temp;
}

int[] ExpEnemy(int[] tempEnemyArray, int chapterCount)                                                                                  //Метод прокачки врагов                         +
{
    for (int i = 0; i < chapterCount; i++)
    {
        tempEnemyArray[0] += 20;                          //Увеличение здоровья
        tempEnemyArray[1] += 2;                           //Увеличение атаки
        tempEnemyArray[2] += 1;                           //Увеличение защиты
    }
    return tempEnemyArray;
}

int[] ExpHero(int[] tempHeroArray, int chapterCount)                                                                                    //Метод прокачки героя                          +
{
    for (int i = 0; i < chapterCount; i++)
    {
        tempHeroArray[0] += 15;                          //Увеличение здоровья
        tempHeroArray[1] += 1;                           //Увеличение атаки
        tempHeroArray[2] += 1;                           //Увеличение защиты
    }
    return tempHeroArray;
}

int[] ExpHeroChange(int[] hero)                                                                                                         //Метод дополнительной раскачки                 +++
{
    int whileNum;
    string answer;

    do
    {
        Console.WriteLine("Какую дисциплину героя улучшить? Нажми цифру");
        Console.WriteLine("1 - Здоровье +20");
        Console.WriteLine("2 - Атака +3");
        Console.WriteLine("3 - Защита +2");
        answer = Console.ReadLine();
        whileNum = answer == "1" || answer == "2" || answer == "3"? 1:0;
        if (whileNum == 0) Console.Clear();
    }
    while (whileNum != 1);

    if (answer == "1") hero[0] += 20;
    else if (answer == "2") hero[1] += 3;
    else if (answer == "3") hero[2] += 2;

    return hero;
}

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


//Методы связанные с магазином 

int Salary(int[] enemy, int wallet)                                                                                                     //Метод начисления денег                    ++++
{
    if (enemy[0] == 130)
    {
        wallet += 130;

        Console.WriteLine("За убийство скелета вам начисленно +130 монет");
    } 

    else if (enemy[0] == 150)
    {
        wallet += 150;

        Console.WriteLine("За убийство зомби вам начисленно +150 монет");
    } 

    Console.WriteLine();
    Console.WriteLine("Для продолжения нажмите клавишу 'Enter'");
    Console.ReadLine();

    return wallet;
}

int Market(int[] hero, int wallet, int chapterCount)                                                                                    //Метод Магазин                             ++++
{
    int[] tempHeroArray = new int[hero.Length];
    FillArray(hero, tempHeroArray);
    ExpHero(tempHeroArray, chapterCount);

    int leatherArmor = 1;
    int advancedSword = 1;
    
    string answer;

    while (true)
    {
        PrintHero(tempHeroArray);
        Console.WriteLine($"У вас {wallet} золотых монет");
        Console.WriteLine();
        Console.WriteLine("Что вы хотите купить?");
        if (leatherArmor == 1 && chapterCount == 2) Console.WriteLine("1 - Кожанный доспех (защита +2) - за 150 монет");
        if (advancedSword == 1 && chapterCount == 2) Console.WriteLine("2 - Продвинутый меч (атака +2) - за 200 монет");
        Console.WriteLine("3 - зелье здоровья за 25 монет (1 штука)");
        Console.WriteLine("0 - Уйти из магазина");
        answer = Console.ReadLine();

        if (answer == "1")
        {
            if (chapterCount == 2)
            {
                if (leatherArmor == 0) Console.WriteLine("Другой брони нет, приходите завтра!");

                else if (leatherArmor == 1)
                {
                    if (wallet >= 150)
                    {
                        hero[2] += 2;
                        leatherArmor--;
                        tempHeroArray[2] += 2;
                        wallet -= 150;

                        Console.WriteLine($"Вы купили кожанный доспех. Теперь ваша броня {tempHeroArray[2]}");
                    }

                    else Console.WriteLine("У вас недостаточно денег!");
                }
            }
            Console.WriteLine();
        }

        else if (answer == "2")
        {
            if (chapterCount == 2)
            {
                if (advancedSword == 0) Console.WriteLine("Другого оружия нет, приходите завтра!");

                else if (advancedSword == 1)
                {
                    if (wallet >= 200)
                    {
                        hero[1] += 2;
                        advancedSword--;
                        tempHeroArray[1] += 2;
                        wallet -= 200;

                        Console.WriteLine($"Вы купили продвинутый меч. Теперь ваша атака {tempHeroArray[1]}");
                    }

                    else Console.WriteLine("У вас недостаточно денег!");
                }
            }
            Console.WriteLine();
        }

        else if (answer == "3")
        {
            if (wallet >= 25)
            {
                if (hero[3] < 10)
                {
                    hero[3]++;
                    wallet -= 25;
                    tempHeroArray[3]++;

                    Console.WriteLine("Вы купили зелье здоровья");
                }

                else Console.WriteLine("У вас не может быть больше 10 зелий!");
            }

            else Console.WriteLine("У вас недостаточно денег!");

            Console.WriteLine();
        }

        else if (answer == "0") break;
    }
    return wallet;
}

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


//Кармические методы

string KarmaFoolProof()                                                                                                                 //Метод защиты от дурака для изменения кармы          +++
{
    int whileNum;
    string answer;

    do
    {
        Console.WriteLine("Тебе нужно сказать 'ДА' или 'НЕТ' ");
        answer = Console.ReadLine().ToLower();
        whileNum = answer == "да" || answer == "нет"? 1:0;
    }
    while (whileNum != 1);

    return answer;
}

int ChangingKarma(int karma, int chapterCount, int[] hero)                                                                              //Метод изменения кармы                               +++++
{
    string answer;

    if (chapterCount == 0)
    {
        Console.WriteLine("Проходя мимо черепа скелета, наш герой заметил блеск на его зубах! Зубы оказались золотые!");
        Console.WriteLine("Вырвать зубы у усопшего, золото ведь живым важнее?");
        Console.WriteLine();

        answer = KarmaFoolProof();

        Console.WriteLine();

        if (answer == "да")
        {
            karma--;

            Console.WriteLine("Вырвать зубы оказалось проще, чем кажется. Феофил брезгливо засунул их в карман и пошел дальше, как ни в чем не бывало!");
            Console.WriteLine("Карма героя уменьшилась на -1");
        }

        else if (answer == "нет")
        {
            Console.WriteLine("Золото конечно хорошо, но вырывать зубы у мертвеца это полное дно. Вздохнув герой медленной побрел до дома.");
            Console.WriteLine("Проходя мимо болота, Феофил услышал детский крик. Стоит ли идти в это гиблое место, мало ли какой нечисти?");
            Console.WriteLine();

            answer = KarmaFoolProof();
            
            Console.WriteLine();

            if (answer == "да")
            {
                karma++;

                Console.WriteLine("Раздвинув камыш, герой увидел маленького мальчика, тонущего в болоте. Он схватил рядом лежащее бревно и кинулся на помощь.");
                Console.WriteLine("Ребенка удалось спасти, это был сын местного охотника. И когда ребенок успокоился, то со всех ног побежал домой, а Феофил пошел дальше!");
                Console.WriteLine("Карма героя увеличелась на +1");
            }

            else Console.WriteLine("Да ну нафиг! - подумал наш герой и, посвистывая, пошел дальше!");
        }
    }

    else if (chapterCount == 2)
    {
        Console.WriteLine("Герой брел по заросшей травой тропинке, по которой явно уже давно никто не ходил. И вдруг откуда не возьмись появился маленький человек в зеленой потрепанной одежде и с отвратительной рожей!");
        Console.WriteLine("Он, поприветствовав нашего героя, назвал себя Леприконом. И после жалоб на кризис, плохие дела в бизнесе и повышения НДС до 20%, предложил обменять четырёх листный клевер на удачу, в обмен на больший запас еды Феофила");
        Console.WriteLine("Все это попахивало очередным разводом и Феофил задавал себе вопрос: стоит ли совершать обмен, это же всего лишь кусок травы?");
        Console.WriteLine();

        answer = KarmaFoolProof();

        Console.WriteLine();

        if (answer == "да")
        {
            hero[4]++;
            karma++;

            Console.WriteLine("Герой согласился и леприкон невероятно обрадовался! Тепрь у него есть запас еды на несколько дней, а там глядишь и образуется. После этого он отдал клевер и исчез, как и появился!");
            Console.WriteLine("Герой аккуратно положил клевер в карман. +1 к удаче он не почувствовал, но то что карма улучшилась, это факт.");
            Console.WriteLine("Карма героя увеличелась на +1");
            Console.WriteLine("Удача героя увеличилась на +1");
        }

        else if (answer == "нет")
        {
            Console.WriteLine("'Разводишь ты меня мелкий, не буду я с тобой меняться!' ответил герой и уже хотел пойти дальше, как леприкон разозлился и злобно крикнул 'Что б ты на меня стал похож, жлобина!'");
            Console.WriteLine("Феофил посмотрел на леприкона, и понял, что его ещё не разу в жизни никто так грубо не оскрблял.");
            Console.WriteLine("Герой разозлился и подумал: 'А не заколоть ли мне маленького уродца, как свинью!'");
            Console.WriteLine();

            answer = KarmaFoolProof();

            if (answer == "да")
            {

                hero[4]++;
                karma--;

                Console.WriteLine("Феофил в мгновение ока вытащил меч и ударил леприкона прямо в сердце! После, он извлек из мертвой руки клевер и пошел своей дорогой!");
                Console.WriteLine("Карма героя уменьшилась на -1");
                Console.WriteLine("Удача героя увеличилась на +1");
            }

            else if (answer == "нет") Console.WriteLine("Феофил подумал, что лучше просто уйти, ведь леприкон каждый день по несколько раз страдает глядя в зеркало!");
        }
    }

    Console.WriteLine();
    Console.WriteLine("Для продолжения нажмите клавишу 'Enter'");
    Console.ReadLine();

    return karma;
}

int[] KarmicConsequences(int[] hero, int chapterCount, int karma)                                                                       //Кармические последствия                             ++++
{
    Console.WriteLine();

    if (chapterCount == 1)
    {
        Console.WriteLine("Наутро, наш отдохнувший герой вышел из дома, потянулся и полный сил и храбрости отправился в путь");
        Console.WriteLine();

        if (karma == 0) Console.WriteLine("Он настолько задрал голову, что не заметил как наступил в кучу теплого навоза и крепко выругавшись, двинулся дальше.");

        else if (karma < 0)
        {
            hero[1] += 3;

            Console.WriteLine("По пути Феофил вспомнил о золотых зубах и пошел к кузнецу. Немного поморщившись кузнец взялся за дело и перековал меч. Ваша атака увеличена на +3");
        }

        else if (karma > 0)
        {
            hero[2] += 3;

            Console.WriteLine("И вдру к Феофилу подбежал ранее спасенный им мальчик и подарил охотничью куртку своего отца. Это была добротная вещь и герой с удовольствием ее надел. Ваша защита увеличена на +3");
        }
    }

    Console.WriteLine();
    Console.WriteLine("Для продолжения нажмите клавишу 'Enter'");
    Console.ReadLine();

    return hero;
}

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


//Вызов методов

HEAD();                                                                                         //Вызов главного метода
Signature();                                                                                    //Вызов создателей

//Вопрос 1. Как сохранить игру?
//Вопрос 2. Как bool и Random соединить, чтобы случайно выпадали true или false?