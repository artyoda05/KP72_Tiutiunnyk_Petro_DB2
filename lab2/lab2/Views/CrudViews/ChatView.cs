using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using ConsoleTableExt;
using lab2.Models;

namespace lab2.Views.CrudViews
{
    class ChatView : CrudView<Chat>
    {
        public ChatView() : base("Chats")
        {
        }

        public override Chat Create()
        {
            Console.WriteLine("\n\rInput tag");
            var tag = Console.ReadLine();
            Console.WriteLine("Input name");
            var name = Console.ReadLine();
            return new Chat(0, tag, name);
        }

        public override void ShowReadResult(Chat data)
        {
            Console.WriteLine("Result:");
            ConsoleTableBuilder.From(DataToDataTable(new List<Chat> { data }))
                .WithFormat(ConsoleTableBuilderFormat.Alternative)
                .ExportAndWriteLine();
            Console.ReadKey();
        }

        public override Chat Update(Chat entity)
        {
            Console.WriteLine($"Input tag. Old one: {entity.Tag}");
            entity.Tag = Console.ReadLine();
            Console.WriteLine($"Input name. Old one: {entity.Name}");
            entity.Name = Console.ReadLine();
            Console.WriteLine($"Input bio. Old one: {entity.Bio ?? "none"}");
            entity.Bio = Console.ReadLine();
            return entity;
        }

        protected override DataTable DataToDataTable(List<Chat> data)
        {
            var dataTable = new DataTable("Users");
            dataTable.Columns.Add(new DataColumn("Id", typeof(long)));
            dataTable.Columns.Add(new DataColumn("Tag", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Name", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Bio", typeof(string)));
            if (data.Count == 0)
            {
                var row = dataTable.NewRow();
                row.ItemArray = new object[] { -1, "Empty", "Empty", "Empty" };
                dataTable.Rows.Add(row);
            }
            else
                foreach (var el in data)
                {
                    var row = dataTable.NewRow();
                    row.ItemArray = new object[]
                    {
                        el.Id,
                        el.Tag,
                        el.Name ,
                        el.Bio
                    };
                    dataTable.Rows.Add(row);
                }

            return dataTable;
        }
    }
}
