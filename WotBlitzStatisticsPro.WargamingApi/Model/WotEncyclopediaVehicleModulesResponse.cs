namespace WotBlitzStatisticsPro.WargamingApi.Model
{
    public class WotEncyclopediaVehicleModulesResponse
    {
        [JsonPropertyName("suspensions")]
        public WotEncyclopediaVehicleModule[]? Suspensions { get; set; }
        [JsonPropertyName("engines")]
        public WotEncyclopediaVehicleModule[]? Engines { get; set; }
        [JsonPropertyName("guns")]
        public WotEncyclopediaVehicleModule[]? Guns { get; set; }
        [JsonPropertyName("turrets")]
        public WotEncyclopediaVehicleModule[]? Turrets { get; set; }
    }

    public class WotEncyclopediaVehicleModule
    {
        [JsonPropertyName("module_id")]
        public long? ModuleId { get; set; }
        [JsonPropertyName("tier")]
        public int? Tier { get; set; }
        [JsonPropertyName("tanks")]
        public int[]? Tanks { get; set; }
        [JsonPropertyName("nation")]
        public string? Nation { get; set; }
        [JsonPropertyName("weight")]
        public long? Weight { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }
}

/*
suspensions

"name": "T-34 mod. 1941",
"weight": 8000,
"nation": "ussr",
"load_limit": 31300,
"traverse_speed": 43,
"tanks": [
1
],
"tier": 4,
"module_id": 2
---
engines

"name": "V-2",
"weight": 750,
"nation": "ussr",
"power": 400,
"fire_chance": 0.15,
"tanks": [
1,
257,
2049
],
"tier": 5,
"module_id": 5
---
guns

name": "76 mm L-11",
"weight": 660,
"shells": [
{
"type": "ARMOR_PIERCING",
"penetration": 85,
"damage": 140
},
{
"type": "HOLLOW_CHARGE",
"penetration": 115,
"damage": 120
}
],
"nation": "ussr",
"dispersion": 0.51,
"tanks": [
1
],
"aim_time": 2.86,
"tier": 4,
"module_id": 4

---
turrets

"name": "T-34 mod. 1940",
"weight": 3200,
"view_range": 200,
"armor": {
"front": 55,
"sides": 55,
"rear": 55
},
"hp": 124,
"nation": "ussr",
"tanks": [
1
],
"traverse_right_arc": 180,
"tier": 4,
"module_id": 3,
"traverse_left_arc": 180
*/