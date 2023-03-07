using AppNET.Domain.Entities;
using AppNET.Domain.Entities.Base;
using AppNET.Domain.Interfaces;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace AppNET.Infrastructure.IOToTXT
{
    public class TextFileRepository<T> : IRepository<T> where T : BaseEntity
    {
        private static string FileName
        {
            get
            {
                return typeof(T).FullName.Replace(".", "") + ".txt";
            }
        }
        private static List<T> list  =new List<T>();
        private static void LoadListFromFile()
        {
            if(!File.Exists(FileName))
            {
                list=new List<T>();
                return;
            }

            var json=File.ReadAllText(FileName);
            list = JsonSerializer.Deserialize<List<T>>(json); 
        }
        private static void WriteListToTxt()
        {
            var jsonText=JsonSerializer.Serialize(list);
            File.WriteAllText(FileName, jsonText);
        }
        static TextFileRepository()
        {
            LoadListFromFile();
        }
        public T Add(T entity)
        {
           LoadListFromFile();
            list.Add(entity);
            WriteListToTxt();
            return entity;
        }

        public T GetById(int id)
        {
           LoadListFromFile();
            var entity = list.FirstOrDefault(x => x.Id == id);
            return entity;
        }

        public ICollection<T> GetList(Func<T, bool> expression = null)
        {
             LoadListFromFile();
            if(expression == null)
            {
                return list;
            }
            else
            {
                return list.Where(expression).ToList();
            }
        }

        public bool Remove(int id)
        {
            LoadListFromFile();
           var deletedEntity = list.FirstOrDefault(x => x.Id == id);
            if (deletedEntity!= null)
            {
                list.Remove(deletedEntity);
                WriteListToTxt();
                return true;
            }
            return false;
        }

        public T Update(int id, T entity)
        {
           if(id!= entity.Id)
                throw new ArgumentException("Id değerleri uyuşmuyor");
            

           LoadListFromFile();
            var updatedEntity= list.FirstOrDefault(x => x.Id == id);
            if (updatedEntity != null)
                throw new ArgumentException("Güncellemek istenilen varlık bulunamadı");

            list.Remove(updatedEntity);
            list.Add(entity);
            WriteListToTxt();
            return entity;
        }

    
    }
}