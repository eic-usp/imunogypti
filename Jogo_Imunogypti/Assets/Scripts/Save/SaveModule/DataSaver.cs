using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;

// The T type is the class with the contents to be saved
public class DataSaver<T> where T : new()
{

    // Will tha file be encrypted?
    private bool encrypt;

    // Must have a save path for the target platform, so the game can save to a file at that location.
        // On Windows, this path points to C:/Users/<user>/AppData/LocalLow/<companyname>
        // On iOS, this path points to /var/mobile/Containers/Data/Application/<guid>/Documents
        // On Android (most phones), this path points to /storage/emulated/0/Android/data/<packagename>/files
    private string prePath;

    // The name of the file save
    private string fileName;

    private int encryptionKey;

    private bool saveHistory;

    private int numberOfSaves;

    private string config = "fxv.meta";

    // DataSaver constructor arguments:
    //      fileName: the name of the file save
    //      encrypt: will tha file be encrypted?
    public DataSaver(string fileName, bool encrypt, bool saveHistory = false){
        //Defines the default path of saving and loading for the game, and the file name. Usable in any platform
        if(fileName == config){
            config = "f" + config;
        }
        this.numberOfSaves = 0;
        this.saveHistory = saveHistory;
        if(saveHistory){
            // This is a workaround to allow savefile persistance on itch.io WebGL builds
            #if UNITY_WEBGL
            this.prePath = System.IO.Path.Combine(System.IO.Path.Combine("/idbfs", Application.productName) , fileName);
            Directory.CreateDirectory(System.IO.Path.Combine(System.IO.Path.Combine("/idbfs", Application.productName), fileName));
            this.config = System.IO.Path.Combine(System.IO.Path.Combine("/idbfs", Application.productName), fileName, this.config);
            #else
            
            this.prePath = System.IO.Path.Combine(Application.persistentDataPath, fileName);
            Directory.CreateDirectory(System.IO.Path.Combine(Application.persistentDataPath, fileName));
            this.config = System.IO.Path.Combine(Application.persistentDataPath, fileName, this.config);
            #endif
            if(File.Exists(this.config)){
                using (TextReader reader = File.OpenText(this.config))
                {
                    this.numberOfSaves = int.Parse(reader.ReadLine());
                    reader.Close();
                }
            }
        }
        else{
            #if UNITY_WEBGL
            this.prePath = System.IO.Path.Combine("/idbfs", Application.productName);
            Directory.CreateDirectory(prePath);
            #else
            this.prePath = Application.persistentDataPath;
            #endif
        }
        this.fileName = fileName;
        this.encrypt = encrypt;
        this.encryptionKey = 1612;

    }

    // Overwrite the current file save with the contents of data argument
    public void SaveData(T data, int index = -1){
        string jsonString = JsonUtility.ToJson(data);
		if(encrypt){
            jsonString = EncryptDecrypt(jsonString);
        }
        string localFilePath = this.fileName;
        if(this.saveHistory){
            if(index == -1){
                localFilePath = numberOfSaves++ + localFilePath;
                UpdateConfig();
            }
            else if(index >= 0){
                localFilePath = index + localFilePath;
            }
            else{
                throw new System.ArgumentOutOfRangeException("Index cant be negative");
            }
        }
        localFilePath = System.IO.Path.Combine(this.prePath, localFilePath);
		using (StreamWriter streamWriter = File.CreateText (localFilePath)){
            streamWriter.Write (jsonString);
            streamWriter.Close();
		}
    }

    // Load content from file save, and returns it
    // It returns the data, if the file exists, otherwise, returns a new object (from the constructor without parameters), if the constructor is not defined, it initializes each attribute with the defualt value (0, false or null)
    // If the file is corrupted, it gets deleted, and a backup file is saved in the same folder, and the function return is null
    public T LoadData(int index = -1){
        T data = new T();

        string localFilePath = this.fileName;

        if(this.saveHistory){
            if(this.numberOfSaves == 0){
                return data;
            }
            if(index == -1){
                localFilePath = (this.numberOfSaves-1) + localFilePath;
            }
            else if(index >= 0){
                localFilePath = index + localFilePath;
            }
            else{
                throw new System.ArgumentOutOfRangeException("Index cant be negative");
            }
        }
        localFilePath = System.IO.Path.Combine(this.prePath, localFilePath);
        if(File.Exists(localFilePath)){
            try{
                using (StreamReader streamReader = File.OpenText (localFilePath)){
                    string jsonString = streamReader.ReadToEnd ();
                    if(jsonString.Length>0){
                        if(encrypt){
                            jsonString = EncryptDecrypt(jsonString);
                        }
                        data = JsonUtility.FromJson<T> (jsonString);
                    }
                    streamReader.Close();
                }
            }
            catch {
                string debug = ".debug";
                int num = 0;
                while(File.Exists(localFilePath+debug+num)){
                    num++;
                }
                Debug.LogWarning("O arquivo de save esta corrompido e foi deletado, um backup para debug foi gerado e se encontre em: \"" + localFilePath+debug+num+ "\"" );
                File.Copy(localFilePath,localFilePath+debug+num);
                File.Delete(localFilePath);
                throw new System.Exception("O arquivo de save esta corrompido e foi deletado, um backup para debug foi gerado e se encontre em: \"" + localFilePath+debug+num+ "\"");
            }
        }
        else{
            Debug.LogWarning("Arquivo de save inexistente: \"" + fileName + "\". Um novo arquivo foi criado e seu jogo será resetado");
        }
        return data;
    }

    public List<T> LoadAllData(){
        int counter = this.numberOfSaves-1;
        string localFilePath;
        List<T> lt = new List<T>();
        while(counter >= 0){
            localFilePath =  System.IO.Path.Combine(this.prePath, counter + this.fileName);
            if(File.Exists(localFilePath)){
                lt.Add(LoadData(counter));
            }
            counter--;
        }

        return lt;
    }

    // Check if there is a file save
    public bool SaveExists(int index = -1){
        string localFilePath = this.fileName;
        if(this.saveHistory){
            if(this.numberOfSaves == 0){
                return false;
            }
            if(index == -1){
                localFilePath = (this.numberOfSaves-1) + localFilePath;
            }
            else if(index >= 0){
                if(index < this.numberOfSaves){
                    localFilePath = index + localFilePath;
                }
                else{
                    return false;
                }
            }
            else{
                return false;
            }
        }
        localFilePath = System.IO.Path.Combine(this.prePath, localFilePath);
        return File.Exists(localFilePath);
    }

    // Delete current file save
    public void DeleteSave(int index = -1){
        string localFilePath = this.fileName;
        if(this.saveHistory){
            if(this.numberOfSaves == 0){
                return;
            }
            if(index == -1){
                localFilePath = (--this.numberOfSaves) + localFilePath;
                UpdateConfig();
            }
            else if(index >= 0){
                if(index < this.numberOfSaves){
                    localFilePath = index + localFilePath;
                }
                else{
                    throw new System.ArgumentOutOfRangeException("Save index out of range");
                }
            }
            else{
                throw new System.ArgumentOutOfRangeException("Index cant be negative");
            }
        }
        localFilePath = System.IO.Path.Combine(this.prePath, localFilePath);
        while(!File.Exists(localFilePath) && this.numberOfSaves >= 0){
            localFilePath =  System.IO.Path.Combine(this.prePath, (--this.numberOfSaves) + this.fileName);
        }
        UpdateConfig();
        File.Delete(localFilePath);
    }


    public int NumberOfSaves(){
        int saves = 0;
        int counter = this.numberOfSaves-1;
        string localFilePath;
        while(counter >= 0){
            localFilePath =  System.IO.Path.Combine(this.prePath, counter + this.fileName);
            if(File.Exists(localFilePath)){
                saves++;
            }
            counter--;
        }

        return saves;
    }

    void UpdateConfig(){
        using (StreamWriter streamWriter = File.CreateText (this.config)){
            streamWriter.Write (this.numberOfSaves);
            streamWriter.Close();
        }
    }

    // Encrypt and decrypt json from file
    private string EncryptDecrypt(string szPlainText){
        StringBuilder szInputStringBuild = new StringBuilder(szPlainText);
        StringBuilder szOutStringBuild = new StringBuilder(szPlainText.Length);
        char Textch;
        for (int iCount = 0; iCount < szPlainText.Length; iCount++){
            Textch = szInputStringBuild[iCount];
            Textch = (char)(Textch ^ encryptionKey);
            szOutStringBuild.Append(Textch);
        }
        return szOutStringBuild.ToString();
    }

}