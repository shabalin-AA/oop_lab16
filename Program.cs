using lab10;
using lab12;
using System;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using System.Xml.Serialization;



namespace lab16
{
    class Program
    {
        static void TreeSerializeJson(TreeNode<string, Book> root, string fname)
        {
            using (FileStream fs = new FileStream(fname, FileMode.OpenOrCreate))
            {
                JsonSerializer.Serialize(fs, root,
                    new JsonSerializerOptions { WriteIndented = true });
            }
        }


        static TreeNode<string, Book> TreeDeserializeJson(string fname)
        {
            using (FileStream fs = new FileStream(fname, FileMode.OpenOrCreate))
            {
                string jsonData = File.ReadAllText(fname);
                return JsonSerializer.Deserialize<TreeNode<string, Book>>(jsonData);
            }
        }


        static void TreeSerializeXml(TreeNode<string, Book> root, string fname)
        {
            using (FileStream fs = new FileStream(fname, FileMode.OpenOrCreate))
            {
                var xmlSerializer = new XmlSerializer(typeof(TreeNode<string, Book>));
                xmlSerializer.Serialize(fs, root);
            }
        }


        static TreeNode<string, Book> TreeDeserializeXml(string fname)
        {
            using (FileStream fs = new FileStream(fname, FileMode.OpenOrCreate))
            {
                var xmlSerializer = new XmlSerializer(typeof(TreeNode<string, Book>));
                return xmlSerializer.Deserialize(fs) as TreeNode<string, Book>;
            }
        }


        static void Main()
        {
            var tree = new PrintingTree<string, Book>();
            for (int i=0; i < 5; i++)
            {
                tree.Add(i.ToString(), new Book());
            }

            var tree2 = new PrintingTree<string, Book>();
            TreeSerializeJson(tree.Root, "tree.json");
            tree2.Root = TreeDeserializeJson("tree.json");
            Console.WriteLine(tree2);

            TreeSerializeXml(tree.Root, "tree.xml");
            tree2.Root = TreeDeserializeXml("tree.xml");
            Console.WriteLine(tree2);
        }
    }
}