using System.Collections;
using System.Collections.Generic;
using System.IO;
using NPOI.HSSF.UserModel; //xls
using NPOI.XSSF.UserModel; //xlsx
using NPOI.SS.UserModel; //data
using UnityEngine;
using UnityEditor;
using SimpleJson;

public class ImportRPGData
{
    static readonly string excelFilePath =  "Assets/Editor/Data/RPGData.xlsx";
    static readonly string jsonFilePath = "Assets/Resources/Data/";

    [MenuItem("Data/ImportData")]
    public static void ImportPlayerLevelData()
    {
        List<PlayerLevelData> data = new List<PlayerLevelData>();

        using (FileStream stream = File.Open(excelFilePath, FileMode.Open, FileAccess.Read))
        {
            IWorkbook book = new XSSFWorkbook(stream);
            ISheet sheet = book.GetSheetAt(1); //Player Sheet
            for (int i = 2; i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);

                PlayerLevelData temp = new PlayerLevelData();

                temp.level = (int)row.GetCell(0).NumericCellValue;
                temp.maxHP = (int)row.GetCell(1).NumericCellValue;
                temp.baseAttack = (int)row.GetCell(2).NumericCellValue;
                temp.reqExp = (int)row.GetCell(3).NumericCellValue;
                temp.moveSpeed = (float)row.GetCell(4).NumericCellValue;
                temp.turnSpeed = (float)row.GetCell(5).NumericCellValue;
                temp.attackRange = (float)row.GetCell(6).NumericCellValue;

                data.Add(temp);
            }

            string JSONString = SimpleJson.SimpleJson.SerializeObject(data);
            //Debug.Log(JSONString);
            File.WriteAllText(jsonFilePath+"PlayerLevelData.json", JSONString);

            stream.Close();
        }

        EditorUtility.FocusProjectWindow();
        AssetDatabase.Refresh();
    }
}
