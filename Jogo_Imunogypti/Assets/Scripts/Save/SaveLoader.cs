using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveLoader{
    private const string fileName = "SaveFile";
    private static DataSaver<SaveFile> dataSaver = new DataSaver<SaveFile>(fileName, true);
    public static SaveFile saveFile = dataSaver.LoadData();

    public static void SaveGame()
    {
        // // Failsafe para garantir que saveFile não seja null
        // if(saveFile == null)
        //     saveFile = new SaveFile();

        // Avalia a situação atual do jogo e salva todas as informações necessarias
        Debug.Log("Saving game...");
        // saveFile.Save();
        // Escreve as informações no arquivo de save
        dataSaver.SaveData(saveFile);
        Debug.Log("Game saved (...probably)");
    }

//     public static void LoadGame(){
//         // if(!saveFile.HasCheckPoint)
//         // {
//         //     NewGame();
//         // }
//         // else
//         // {
//             // Aplica a situação do save slot atual nos arquivos do jogo
//             saveFile.Load();
//         // }
//     }

//     public static void NewGame()
//     {
//         saveFile.NewGame();
//     }

//     public static void ResetGame(){}
}