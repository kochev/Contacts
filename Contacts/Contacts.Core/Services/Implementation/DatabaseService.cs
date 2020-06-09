using System.Collections.Generic;
using System.Linq;
using MvvmCross;
using MvvmCross.Base;
using Realms;

namespace Contacts.Core.Services.Implementation
{
    public class DatabaseService
    {
        private readonly IMvxMainThreadAsyncDispatcher _dispatcher;

        public DatabaseService()
        {
            _dispatcher = Mvx.IoCProvider.Resolve<IMvxMainThreadAsyncDispatcher>();
            Init();
        }

        public Realm Database { get; set; }

        public IQueryable<T> GetAll<T>() where T : RealmObject
        {
            return Database.All<T>();
        }

        private async void Init()
        {
            await _dispatcher.ExecuteOnMainThreadAsync(() => { Database = Realm.GetInstance(); });
        }

        public async void AddOrUpdate<T>(T @object) where T : RealmObject
        {
            await Database.WriteAsync(realm => realm.Add(@object, true));
        }

        public async void AddOrUpdate<T>(IEnumerable<T> objects) where T : RealmObject
        {
            await Database.WriteAsync(realm =>
            {
                foreach (var o in objects) realm.Add(o, true);
            });
        }

        public void Remove<T>(T @object) where T : RealmObject
        {
            _dispatcher.ExecuteOnMainThreadAsync(() =>
            {
                if (@object == null) return;

                Database.Write(() => { Database.Remove(@object); });
            });
        }
    }
}