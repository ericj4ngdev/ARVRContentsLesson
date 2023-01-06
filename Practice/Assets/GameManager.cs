using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public void Game()
    {
        print("게임매니저가 싱글톤이 되었다!");
    }
}
