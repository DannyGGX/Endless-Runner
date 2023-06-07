using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AccessDataBase : MonoBehaviour
{

    private MongoClient client = new MongoClient("mongodb+srv://danielglanz111:myPassword@cluster1.brhp1nm.mongodb.net/?retryWrites=true&w=majority");
    IMongoDatabase database;
    IMongoCollection<BsonDocument> collection;

    void Awake()
    {
        database = client.GetDatabase("GameData");
        collection = database.GetCollection<BsonDocument>("HighScores");

        // this is a test
        var document = new BsonDocument { { "player1", 10 } };
        collection.InsertOne(document);

        //GetData();
    }

    public async Task<List<Highscore>> GetData()
    {
        var allScoresTask = collection.FindAsync(new BsonDocument());
        var allScoresAwaited = await allScoresTask;

        List<Highscore> highscores = new List<Highscore>();
        foreach(var score in allScoresAwaited.ToList())
        {
            highscores.Add(ConvertJson(score.ToString()));
        }
        return highscores;
    }

    private Highscore ConvertJson(string rawJson)
    {
        Highscore highscore = new Highscore();
        // this is where we manipulate our string


        string stringWithNoID = rawJson.Substring(rawJson.IndexOf("),") + 4);
        string username = stringWithNoID.Substring(0, stringWithNoID.IndexOf(":")-2);
        int scoreStrStart = stringWithNoID.IndexOf(":") + 2;
        string scoreStr = stringWithNoID.Substring(scoreStrStart, stringWithNoID.IndexOf("}") - scoreStrStart - 3);

        highscore.PlayerName = username;
        highscore.Score = int.Parse(scoreStr);
        return highscore;
    }

    public async void SaveData(string playerName, int playerScore)
    {
        var document = new BsonDocument { { playerName, playerScore } };
        await collection.InsertOneAsync(document);
    }
}
