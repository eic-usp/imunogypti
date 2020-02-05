//using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
//using System.Data;
using System.Globalization;
using System.Collections.ObjectModel;

[System.Serializable]
public class DynamicTable/* : DataTable*/
{
    // Declaração de subclasses e delegates ------------------
    [System.Serializable]
    public class ColDescription : RotaryHeart.Lib.SerializableDictionary.SerializableDictionaryBase<string, int>{ }

    [System.Serializable]
    public class TableRow {
        public int Count { get{ return (elements != null)? elements.Length : 0; }}
        [SerializeField] private string[] elements;
        [HideInInspector,SerializeField] string[] colTypes;
        [HideInInspector,SerializeField] ColDescription Col;

        public TableRow(string line, ColDescription col, string[] types){
            elements = line.Split(splitCharacter);
            Col = col;
            colTypes = types;
        }

        private int GetColNumber(string colName, string assembQualName){
            if(colTypes[Col[colName]] == assembQualName)
                return Col[colName];
            else
                return -1;
        }

        public T Field<T>(string colName){
            int colNumber = GetColNumber(colName, typeof(T).AssemblyQualifiedName);
            if(colNumber < 0) return default(T);

            string description = elements[colNumber];

            if(typeof(T) == typeof(int)){
                return (T)(object)int.Parse(description, CultureInfo.InvariantCulture.NumberFormat);
            }else if(typeof(T) == typeof(long)){
                return (T)(object)long.Parse(description, CultureInfo.InvariantCulture.NumberFormat);
            }else if(typeof(T) == typeof(string)){
                return (T)(object)description;
            }else if(typeof(T) == typeof(float)){
                return (T)(object)float.Parse(description, CultureInfo.InvariantCulture.NumberFormat);
            }else if(typeof(T) == typeof(bool)){
                return (T)(object)bool.Parse(description);
            }else /*if(typeof(T) == typeof(Color))*/{
                Color newColor = new Color();
                ColorUtility.TryParseHtmlString(description, out newColor);
                return (T)(object)newColor;
            // }else if(typeof(T) == typeof(Element) || typeof(T) == typeof(DamageType)){
            //     int value = int.Parse(description, CultureInfo.InvariantCulture.NumberFormat);
            //     if(typeof(T) == typeof(Element))
            //         return (T)(object)(Element)value;
            //     else
            //         return (T)(object)(DamageType)value;
            // }else{
            //     return (T)(object)AssetManager.LoadAsset(description, typeof(T));
            }
        }
    }

    // Variaveis/Proriedades -------------------------
    private const char splitCharacter = ';';
    [SerializeField] private string[] colTypes;
    [SerializeField] private ColDescription Col;
    [SerializeField] private TableRow[] rows;
    public ReadOnlyCollection<TableRow> Rows {
        get{
            if(rows == null)
                return new ReadOnlyCollection<TableRow>(new List<TableRow>());
            else
                return (new List<TableRow>(rows)).AsReadOnly();
        }
    }

    // Metodos ---------------------------------------
    public static DynamicTable Create(TextAsset textFile){
        if(textFile == null){
            Debug.Log("Must pass a valid textFile as parameter");
            return null;
        }else
            return new DynamicTable(textFile);
    }

    protected static private string GetQualifiedName(string typeName){
        switch(typeName){
            case "string":
                return typeof(string).AssemblyQualifiedName;
            case "int":
                return typeof(int).AssemblyQualifiedName;
            case "long":
                return typeof(long).AssemblyQualifiedName;
            case "float":
                return typeof(float).AssemblyQualifiedName;
            case "bool":
                return typeof(bool).AssemblyQualifiedName;
            case "Color":
                return typeof(Color).AssemblyQualifiedName;
            // case "Enemy":
            //     return typeof(Enemy).AssemblyQualifiedName;
            // case "Party":
            //     return typeof(Party).AssemblyQualifiedName;
            // case "Skill":
            //     return typeof(Skill).AssemblyQualifiedName;
            // case "Element":
            //     return typeof(Element).AssemblyQualifiedName;
            // case "DamageType":
            //     return typeof(DamageType).AssemblyQualifiedName;
            default:
                return typeof(string).AssemblyQualifiedName;
        }
    }


    protected DynamicTable(TextAsset textFile){
        string[] lines = Regex.Split(textFile.text, "\n|\r\n|\r");
        string[] colNames = lines[0].Split(splitCharacter);
        string[] colTypeNames = lines[1].Split(splitCharacter);

        Col = new ColDescription();
        colTypes = new string[colTypeNames.Length];
        rows = new TableRow[lines.Length - 2];

        int nCols = colNames.Length;
        for(int i = 0; i < colNames.Length; i++){
            Col.Add(colNames[i], i);
            colTypes[i] = GetQualifiedName(colTypeNames[i]);
        }

        for(int i = 2; i < lines.Length; i++){
            rows[i-2] = new TableRow(lines[i], Col, colTypes);
        }
    }
}