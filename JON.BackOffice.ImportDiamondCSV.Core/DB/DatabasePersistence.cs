using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace JON.BackOffice.ImportDiamondCSV.Core.DB
{
    public class DatabasePersistence : IDatabasePersistence
    {
        private List<IDiamond> DiamondList;

        public void SaveOrUpdate()
        {
            using (var db = new JONetEntitiesAdmin())
            {
                foreach (var diamond in DiamondList)
                {
                    var exists = Queryable.Where(db.INV_DIAMONDS_INVENTORY, x => x.diamondid == diamond.diamondid).SingleOrDefault();
                    if (exists!=null)
                    {
                        

                        var result = diamond as INV_DIAMONDS_INVENTORY;
                        result.id = exists.id;

                        db.INV_DIAMONDS_INVENTORY.Detach(exists);
                        db.INV_DIAMONDS_INVENTORY.Attach(result);
                        db.ObjectStateManager.ChangeObjectState(result, EntityState.Modified);
                    }
                    else
                    {
                        db.INV_DIAMONDS_INVENTORY.AddObject(diamond as INV_DIAMONDS_INVENTORY);
                    }
                    
                }

                db.SaveChanges();

            }
        }
        public void AddSupplierDiamondList(List<IDiamond> list)
        {
            this.DiamondList = list;
        }
    }
}
