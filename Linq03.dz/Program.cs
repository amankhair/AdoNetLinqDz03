using Linq03.dz.Model;
using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Linq03.dz
{
    class Program
    {
        static CrcmsDB db = new CrcmsDB();

        static void Main(string[] args)
        {
            //Task1();
            //Task2();
            //Task3();
            //Task4();
            //Task5();
            //Task6();
            //Task7();

            //Написать запросы к выгруженным XML файлам:
            //Task8();
            //Task9();
            //Task10();
            //Task11();

            //Выгрузить статистику на экран используя данные из XML файла пункта C
            Task12();
        }

        static void Task1()
        {
            //Все зоны/участки которые принадлежат PavilionId = 1, 
            //при этом каждая зоны должна находиться в отдельном XML файле с наименованием PavilionId.

            var data = db.Areas.Where(w => w.PavilionId == 1);

            foreach (var item in data)
            {
                DirectoryInfo dir = new DirectoryInfo(@".\01 PavilionId\");
                dir.Create();

                XDocument xDoc = new XDocument(
                     new XElement("Areas",
                        new XElement("Area",
                            new XAttribute("type", "Table"),
                            new XElement("AreaId", item.AreaId),
                            new XElement("TypeArea", item.TypeArea),
                            new XElement("Name", item.Name),
                            new XElement("ParentId", item.ParentId),
                            new XElement("NoSplit", item.NoSplit),
                            new XElement("AssemblyArea", item.AssemblyArea),
                            new XElement("FullName", item.FullName),
                            new XElement("MultipleOrders", item.MultipleOrders),
                            new XElement("HiddenArea", item.HiddenArea),
                            new XElement("IP", item.IP),
                            new XElement("PavilionId", item.PavilionId),
                            new XElement("TypeId", item.TypeId),
                            new XElement("OrderExecution", item.OrderExecution),
                            new XElement("Dependence", item.Dependence),
                            new XElement("WorkingPeople", item.WorkingPeople),
                            new XElement("ComponentTypeId", item.ComponentTypeId),
                            new XElement("GroupId", item.GroupId),
                            new XElement("Segment", item.Segment)
                            )));

                string fileName = item.Name;

                if (fileName.Contains('/'))
                {
                    fileName = fileName.Replace("/", "");
                }

                xDoc.Save(dir + fileName + "PavilionId.xml");
            }
        }

        static void Task2()
        {
            //Используя Directory класс, создать папки с название зон/участков, 
            //в каждую папку выгрузить XML файл на основе данных их таблицы.

            foreach (Area item in db.Areas)
            {
                DirectoryInfo dir = new DirectoryInfo(@".\02 AreaDirectory\" + item.Name);
                dir.Create();

                XDocument xDoc = new XDocument(
                    new XElement("Areas",
                        new XElement("Area",
                            new XAttribute("type", "Table"),
                            new XElement("AreaId", item.AreaId),
                            new XElement("TypeArea", item.TypeArea),
                            new XElement("Name", item.Name),
                            new XElement("ParentId", item.ParentId),
                            new XElement("NoSplit", item.NoSplit),
                            new XElement("AssemblyArea", item.AssemblyArea),
                            new XElement("FullName", item.FullName),
                            new XElement("MultipleOrders", item.MultipleOrders),
                            new XElement("HiddenArea", item.HiddenArea),
                            new XElement("IP", item.IP),
                            new XElement("PavilionId", item.PavilionId),
                            new XElement("TypeId", item.TypeId),
                            new XElement("OrderExecution", item.OrderExecution),
                            new XElement("Dependence", item.Dependence),
                            new XElement("WorkingPeople", item.WorkingPeople),
                            new XElement("ComponentTypeId", item.ComponentTypeId),
                            new XElement("GroupId", item.GroupId),
                            new XElement("Segment", item.Segment)
                        )));

                string fileName = item.Name;

                if (fileName.Contains('/'))
                {

                    fileName = fileName.Replace("/", "");

                }
                xDoc.Save(dir + @"\" + fileName + "PavilionId.xml");
            }
        }

        static void Task3()
        {
            //Выгрузить XML файл только тех участков, которые не имеют дочерних элементов (ParentId = 0);

            XDocument xDoc = new XDocument();
            XElement rootElement = new XElement("Areas");

            DirectoryInfo dir = new DirectoryInfo(@".\03 AreaParentId\");
            dir.Create();

            var data = db.Areas.Where(w => w.ParentId == 0);

            foreach (var item in data)
            {
                XElement elem = new XElement("Area",
                            new XAttribute("type", "Table"),
                            new XElement("AreaId", item.AreaId),
                            new XElement("TypeArea", item.TypeArea),
                            new XElement("Name", item.Name),
                            new XElement("ParentId", item.ParentId),
                            new XElement("NoSplit", item.NoSplit),
                            new XElement("AssemblyArea", item.AssemblyArea),
                            new XElement("FullName", item.FullName),
                            new XElement("MultipleOrders", item.MultipleOrders),
                            new XElement("HiddenArea", item.HiddenArea),
                            new XElement("IP", item.IP),
                            new XElement("PavilionId", item.PavilionId),
                            new XElement("TypeId", item.TypeId),
                            new XElement("OrderExecution", item.OrderExecution),
                            new XElement("Dependence", item.Dependence),
                            new XElement("WorkingPeople", item.WorkingPeople),
                            new XElement("ComponentTypeId", item.ComponentTypeId),
                            new XElement("GroupId", item.GroupId),
                            new XElement("Segment", item.Segment)
                        );

                rootElement.Add(elem);

            }
            xDoc.Add(rootElement);
            xDoc.Save(dir + @"\ParentId.xml");
        }

        static void Task4()
        {
            //Выгрузить из таблицы Timer, данные только для зон у которых есть IP адрес, 
            //при этом в XML файл должны войти следующие поля: UserId, Area Name (name из Талицы Area), DateStart


            var data = db.Areas.Where(w => w.IP != null && w.IP != "")
                .Join(db.Timers, ar => ar.AreaId, tm => tm.AreaId, (ar, tm) => new
                {
                    ar.Name,
                    tm.UserId,
                    tm.DateStart
                });

            foreach (var item in data)
            {

                DirectoryInfo dir = new DirectoryInfo(@".\04 AreaTimerJoin\" + item.Name);
                dir.Create();

                XDocument xDoc = new XDocument(
                    new XElement("AreaTimerJoin",
                        new XElement("AreaTimerJoin",
                            new XAttribute("type", "Table"),
                            new XElement("Name", item.Name),
                            new XElement("UserId", item.UserId),
                            new XElement("DateStart", item.DateStart)
                            )));


                string fileName = item.Name;

                if (fileName.Contains('/'))
                {

                    fileName = fileName.Replace("/", "");

                }

                xDoc.Save(dir + @"\" + fileName + ".xml");
            }
        }

        static void Task5()
        {
            //Выгрузить в XML файл, данные из таблицы Timer, у которых нет даты завершения работы DateFinish =null

            var data = db.Timers.Where(w => w.DateFinish == null);

            XDocument xDoc = new XDocument();
            XElement rootElem = new XElement("TimerTable");

            DirectoryInfo dir = new DirectoryInfo(@".\05 Timer_DateFinish_Null\");
            dir.Create();

            foreach (var item in data)
            {
                XElement elem = new XElement(
                        new XElement("Timer",
                            new XElement("TimerId", item.TimerId),
                            new XElement("UserId", item.UserId),
                            new XElement("AreaId", item.AreaId),
                            new XElement("DocumrntId", item.DocumentId),
                            new XElement("DateStart", item.DateStart),
                            new XElement("DateFinish", item.DateFinish),
                            new XElement("DurationInSeconds", item.DurationInSeconds)
                        ));

                rootElem.Add(elem);


            }
            xDoc.Add(rootElem);
            xDoc.Save(dir + @"\Timer.xml");
        }

        static void Task6()
        {
            //Выгрузить весь список выполненных работ из таблицы Timer

            var data = db.Timers.Where(w => w.DateFinish != null);

            XDocument xDoc = new XDocument();
            XElement rootElem = new XElement("TimerTable");

            DirectoryInfo dir = new DirectoryInfo(@".\06 Timer_DateFinish_NotNull\");
            dir.Create();

            foreach (var item in data)
            {
                XElement elem = new XElement(
                    new XElement("Timer",
                        new XElement("TimerId", item.TimerId),
                        new XElement("UserId", item.UserId),
                        new XElement("AreaId", item.AreaId),
                        new XElement("DocumrntId", item.DocumentId),
                        new XElement("DateStart", item.DateStart),
                        new XElement("DateFinish", item.DateFinish),
                        new XElement("DurationInSeconds", item.DurationInSeconds)
                    ));

                rootElem.Add(elem);
            }
            xDoc.Add(rootElem);
            xDoc.Save(dir + @"\Timer.xml");
        }

        static void Task7()
        {
            //Выгрузить данные с таблицы Area в переменную, 
            //на основе данных в этой переменной создать XML файл имеющим Xmlns = «area», 
            //а также namespace - http://logbook.itstep.org/

            var data = db.Areas;

            XDocument xDoc = new XDocument();
            XNamespace itstep = "http://logbook.itstep.org";
            XElement rootElem = new XElement("Areas");


            DirectoryInfo dir = new DirectoryInfo(@".\07 NamespaceFolder\");
            dir.Create();

            foreach (var item in data)
            {
                XElement elem = new XElement(
                        new XElement("Area",
                            new XAttribute(XNamespace.Xmlns + "area", itstep),
                            new XElement("AreaId", item.AreaId),
                            new XElement("TypeArea", item.TypeArea),
                            new XElement("Name", item.Name),
                            new XElement("ParentId", item.ParentId),
                            new XElement("NoSplit", item.NoSplit),
                            new XElement("AssemblyArea", item.AssemblyArea),
                            new XElement("FullName", item.FullName),
                            new XElement("MultipleOrders", item.MultipleOrders),
                            new XElement("HiddenArea", item.HiddenArea),
                            new XElement("IP", item.IP),
                            new XElement("PavilionId", item.PavilionId),
                            new XElement("TypeId", item.TypeId),
                            new XElement("OrderExecution", item.OrderExecution),
                            new XElement("Dependence", item.Dependence),
                            new XElement("WorkingPeople", item.WorkingPeople),
                            new XElement("ComponentTypeId", item.ComponentTypeId),
                            new XElement("GroupId", item.GroupId),
                            new XElement("Segment", item.Segment)
                        ));

                if (item.Name.Contains('/'))
                {
                    item.Name = item.Name.Replace("/", "");
                }

                rootElem.Add(elem);

            }
            xDoc.Add(rootElem);
            xDoc.Save(dir + "Namespace.xml");
        }

        //Написать запросы к выгруженным XML файлам:
        static void Task8()
        {
            //Вывести на экран поля UserId, AreaId, DocumentId из XML файла пункта F

            XDocument xDoc = XDocument.Load(@"C:\Users\Amankeldi Kairbay\source\repos\Linq03.dz\Linq03.dz\bin\Debug\06 Timer_DateFinish_NotNull\Timer.xml");

            //работает
            //foreach (XElement elemnet in xDoc.Element("TimerTable").Elements("Timer"))
            //{
            //    XElement userId = elemnet.Element("UserId");
            //    XElement areaId = elemnet.Element("AreaId");
            //    XElement documentId = elemnet.Element("DocumentId");

            //    Console.WriteLine("UserId: {0}", userId.Value);
            //    Console.WriteLine("AreaId: {0}", areaId.Value);
            //    //Console.WriteLine("DocumentId: {0}", documentId.Value);

            //    if (documentId == null)
            //    {
            //        Console.WriteLine("DocumentId: {0}", "null");
            //    }
            //    else
            //    {
            //        Console.WriteLine("DocumentId: {0}", documentId.Value);
            //    }
            //    Console.WriteLine();
            //}

            //Не работает
            var data = from x in xDoc.Element("TimerTable").Elements("Timer")
                       select new
                       {
                           UserId = x.Element("UserId").Value,
                           AreaId = x.Element("AreaId").Value,
                           DocumentId = x.Element("DocumentId").Value
                       };

            foreach (var item in data)
            {
                Console.WriteLine("UserId: {0}", item.UserId);
                Console.WriteLine("AreaId: {0}", item.AreaId);
                Console.WriteLine("DocumentId: {0}", "null");
                Console.WriteLine();
            }
        }

        static void Task9()
        {
            //Выгрузить все данные из XML пункта F, 
            //заменить при этом в XML файла DateFinish =текущая дата 
            //и сохранить данный XML файл с наименованием – «TimeChangeToday_10.27.2017»

            string xmlName = "Timer";

            XDocument xDoc = XDocument.Load(@"C:\Users\Amankeldi Kairbay\source\repos\Linq03.dz\Linq03.dz\bin\Debug\06 Timer_DateFinish_NotNull\" + xmlName + ".xml");
            XElement rootElement = xDoc.Element("TimerTable");

            foreach (XElement x in rootElement.Elements("Timer").ToList())
            {
                x.Element("DateFinish").Value = DateTime.Now.ToString("s");
            }


            xmlName = "TimeChangeToday_" + DateTime.Now.ToShortDateString();

            xmlName = xmlName.Replace("/", ".");

            xDoc.Save(@"C:\Users\Amankeldi Kairbay\source\repos\Linq03.dz\Linq03.dz\bin\Debug\06 Timer_DateFinish_NotNull\" + xmlName + ".xml");
        }

        static void Task10()
        {
            //Вывести на экран, данные из XML пункта С, из веток – AreaId, Name, FullName, IP

            XDocument xDoc = XDocument.Load(@"C:\Users\Amankeldi Kairbay\source\repos\Linq03.dz\Linq03.dz\bin\Debug\03 AreaParentId\ParentId.xml");
            XElement rootElement = xDoc.Element("Areas");

            //foreach (XElement el in rootElement.Elements("Area"))
            //{
            //    AreaId = el.Element("AreaId").Value;
            //    Name = el.Element("Name").Value;
            //    FullName = el.Element("FullName").Value;
            //    IP = el.Element("IP").Value;

            //    Console.WriteLine("AreaId: {0}", AreaId);
            //    Console.WriteLine("Name: {0}", Name);
            //    Console.WriteLine("FullName: {0}", FullName);
            //    Console.WriteLine("IP: {0}", IP);
            //    Console.WriteLine();
            //}

            var data = from x in xDoc.Element("Areas").Elements("Area")
                       select new
                       {
                           AreaId = x.Element("AreaId").Value,
                           Name = x.Element("Name").Value,
                           FullName = x.Element("FullName").Value,
                           IP = x.Element("IP").Value
                       };

            foreach (var it in data)
            {
                Console.WriteLine("AreaId: {0}", it.AreaId);
                Console.WriteLine("Name: {0}", it.Name);
                Console.WriteLine("FullName: {0}", it.FullName);
                Console.WriteLine("IP: {0}", it.IP);
                Console.WriteLine();
            }
        }

        static void Task11()
        {
            //Выгрузить из XML файла (XML из пункта G) на экран, только те, у которых UserId !=0,
            //а так же нет даты завершения DateFinish =null.

            XDocument xDoc = XDocument.Load(@"C:\Users\Amankeldi Kairbay\source\repos\Linq03.dz\Linq03.dz\bin\Debug\05 Timer_DateFinish_Null\Timer.xml");

            var data = from x in xDoc.Element("TimerTable").Elements("Timer")
                       where x.Element("UserId").Value != "0" && x.Element("DateFinish").Value == null
                       select new
                       {
                           TimerId = x.Element("TimerId").Value,
                           DurationInSeconds = x.Element("DurationInSeconds").Value
                       };

            foreach (var it in data)
            {
                Console.WriteLine("TimerId: {0}", it.TimerId);
                Console.WriteLine("DurationInSeconds: {0}", it.DurationInSeconds);
                Console.WriteLine();
            }
        }

        //Выгрузить статистику на экран используя данные из XML файла пункта C
        static void Task12()
        {
            //a.Количеств не завершенных работ
            //b.Количество работ на каждой зоне с указанием наименования зоны.
            //c.Сумму потраченного времени на каждой зоне
            //d.Данные описанные пунктом выше с «a» по «c» -должны быть отсортированы от большего к меньшему. 

            XDocument xDocArea = XDocument.Load(@"C:\Users\Amankeldi Kairbay\source\repos\Linq03.dz\Linq03.dz\bin\Debug\03 AreaParentId\ParentId.xml");
            XDocument xDocTimer = XDocument.Load(@"C:\Users\Amankeldi Kairbay\source\repos\Linq03.dz\Linq03.dz\bin\Debug\05 Timer_DateFinish_Null\Timer.xml");
            int count = 0;

            var data = xDocArea.Element("Areas").Elements("Area")
                .Join(xDocTimer.Element("TimerTable").Elements("Timer"),
                    area => area.Element("AreaId").Value,
                    timer => timer.Element("AreaId").Value,
                    (area, timer) => new
                    {
                        DateFinishCount = xDocTimer.Element("TimerTable").Elements("Timer")
                            .Where(w => w.Element("AreaId").Value == area.Element("AreaId").Value)
                            .Select(s => s.Element("DateFinish").Value == "").Count(),

                        WorkName = area.Element("Name").Value,

                        WorkCount = xDocTimer.Element("TimerTable").Elements("Timer")
                            .Where(w => w.Element("AreaId").Value == area.Element("AreaId").Value)
                            .Select(s => area.Element("Name").Value != null).Count(),

                    });



            int count2 = 0;
            foreach (var it in data)
            {
                count += it.DateFinishCount;
                count2 += it.WorkCount;
                Console.WriteLine("Name: {0}", it.WorkName);
            }

            Console.WriteLine("Count: {0}", count); //почему-то возвращает 55, должно быть 66
            Console.WriteLine("WorkCount: {0}", count2);

        }
    }
}
