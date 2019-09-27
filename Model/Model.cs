using esoft.ModelView;
using System;

namespace esoft.Model
{
    public static class Model
    {
        public static void UpdateCollections()
        {
            ClientModelView.Update();
            AgentModelView.Update();
            EstateModelView.Update();
            DemandModelView.Update();
            OfferModelView.Update();
            DealModelView.Update();
        }
        public static bool Save(Object obj)
        {
            try
            {
                using (Context db = new Context())
                {
                    db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }
        public static bool Remove(Object obj)
        {
            try
            {
                using (Context db = new Context())
                {
                    db.Entry(obj).Property("isDeleted").CurrentValue = true;
                    db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }
        public static bool Create(Object obj)
        {
            try
            {
                using (Context db = new Context())
                {
                    db.Entry(obj).State = System.Data.Entity.EntityState.Added;
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }
    }
}
