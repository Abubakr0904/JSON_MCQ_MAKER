using System.Text.Json;
while (true)
{
    // ? to'g'ri javob deb qaysidir variant raqamini kiritilganda validatsiya qilish uchun
    var tempBool = true;
    // ? kamida uchta savol o'qish uchun counter
    int c = 3;
    // ? savol o'qish uchun
    string savol;
    // ? variant o'qish uchun
    string option;
    // ? savol
    savol = Console.ReadLine();
    // ? 3 tadan ko'p variant kiritishni xohlash yoki xohlamasligini so'rash uchun
    string a = "1";
    // ? variant o'qish uchun
    Dictionary<string, bool> options = new();
    // ? o'qilgan variantlarni indexlari bilan ko'rsatish uchun listga o'girib olinadi
    var list = options.Keys.ToList();
    // ? mavjud ma'lumotlarni o'qib olish uchun model
    List<QuestionModel> existingData = new();
    
    Console.WriteLine($"YANGI TESTNI KIRITISH BOSHLANDI!");
    
    Console.WriteLine($"SAVOLNI KIRITING: ");
    savol = Console.ReadLine();
    

    // ? 3 ta variant o'qiladi
    Console.WriteLine($"KAMIDA UCHTA VARIANT KIRITING:");
    while(c-- != 0)
    {
        Console.WriteLine($"{options.Count + 1} - VARIANTNI KIRITING");
        option = Console.ReadLine();
        while(option == null || option == string.Empty || !options.TryAdd(option, false))
        {
            Console.WriteLine($"BIR XIL VARIANTLAR YOKI BO'SH MATN KIRITISH MUMKIN EMAS!");
            option = Console.ReadLine();
        }
    }

    // ? yana variant kiritmoqchimi? so'raladi
    while(a == "1")
    {
        Console.WriteLine($"YANA VARIANT KIRITASIZMI?");
        Console.WriteLine($"1: HA, DAVOM ETISH: ISTALGAN BELGI.");
        
        a = Console.ReadLine();
        
        if(a == "1")
        {
            Console.WriteLine($"{options.Count + 1} - VARIANTNI KIRITING");
            option = Console.ReadLine();
            while(option == null || option == string.Empty || !options.TryAdd(option, false))
            {
                Console.WriteLine($"ILTIMOS, TO'G'RI MATN KIRITING!");
                option = Console.ReadLine();
            }
        }
    }

    // ? ko'rsatilgan variantlar orasidan to'g'risini tanlashi so'raladi
    Console.WriteLine($"TO'G'RI JAVOB RAQAMINI KIRITING!");
    for(int i = 0; i < list.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {list[i]}");
    }
    Console.WriteLine();
    Console.WriteLine("TANLANG");

    // ? tanlangan variant raqami validatsiya qilinadi
    while(tempBool)
    {
        var temp = Console.ReadLine();
        if(int.TryParse(temp, out int ans))
        {
            if(ans > 0 && ans <= list.Count)
            {
                tempBool = false;
                options[list[ans-1]] = true;
            }
            else
            {
                Console.WriteLine($"NOTO'G'RI RAQAM TANLANDI. ILTIMOS, QAYTADAN TO'G'RI JAVOBNI QAYTADAN KIRITING!");
                for(int i = 0; i < options.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {list[i]}");
                }
            }
        }
        else
        {
            Console.WriteLine($"XATOLIK! BERILGAN RAQAMLARDAN BIRINI TANLASHINGIZ SO'RALYAPTI! QAYTADAN TANLANG!");
        }
    }

    // ? barcha ma'lumotlar to'g'ri kiritilgandan keyin jsonga qo'shish uchun yangi model yaratadi
    var model = new QuestionModel() { Question = savol, Options = options };

    // ? json file'dan barcha mavjud ma'lumotlarni avval o'qib olinadi
    var jsonData = File.ReadAllText("Questions.json");
    if(!string.IsNullOrWhiteSpace(jsonData))
    {
        existingData = JsonSerializer.Deserialize<List<QuestionModel>>(jsonData) ?? new();
    }

    // ? biz o'qigan ma'lumotlarni modelga qo'shyapti.
    existingData?.Add(model);
    // ? jsonga o'giryapti.
    var readyJson = JsonSerializer.Serialize(existingData);
    
    // ? yangi, o'zgartirilgan jsonni qaytatadan filega yozib qo'yilyapti.
    File.WriteAllText("Questions.json", readyJson);

    Console.WriteLine($"MUVAFFAQQIYATLI SAQLANDI! DAVOM ETISHINGIZ MUMKIN!");
    Console.WriteLine();
}