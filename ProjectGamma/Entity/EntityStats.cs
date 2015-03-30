using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGamma.Entities
{
    public struct TowerStatTable
    {
        public int Type;
        public string Name;
        public string DisplayName;
        public float Damage;
        public float Range;
        public int UnitCost;

        public TowerStatTable(int type, int unitCost, string name, string displayName, float damage, float range)
        {
            this.Type = type;
            this.Name = name;
            this.DisplayName = displayName;
            this.Damage = damage;
            this.Range = range;
            this.UnitCost = unitCost;
        }
    }

    public struct TowerStatUpgrade
    {

    }

    public static class EntityStats
    {
        public static Dictionary<TowerEntityType, TowerStatTable> TowerEntityStatTable = new Dictionary<TowerEntityType, TowerStatTable>()
        {
            { TowerEntityType.Basic, new TowerStatTable(0, 200, "tier1", "Tier 1 Tower", 10f, 20f) }
        };

        public static bool LoadTowerConfig()
        {
            return false;
        }

        public static bool LoadEnemyConfig()
        {
            return false;
        }

        public static bool LoadGameConfig()
        {
            return false;
        }
    }
}
