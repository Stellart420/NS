using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIController : BaseController<UIController>
{
    public List<Screen> screens = new List<Screen>();

    Screen currentScreen = null;

    protected override void Initialization()
    {
        InitUI();
    }

    private void InitUI()
    {
        foreach (var window in screens)
        {
            if (window is StartScreen)
            {
                currentScreen = window;
                window.Open();
            }
            else
                window.Close();
        }

        var game_screen = Get<GameScreen>() as GameScreen;
        PlayerController.Instance.OnHealthChange += game_screen.SetHealth;
        GameController.Instance.ChangeScore += game_screen.SetScore;
    }

    public Screen Get<T>() where T : Screen => screens.OfType<T>().FirstOrDefault();

    public void OpenScreen<T>() where T : Screen
    {
        var screen = screens.OfType<T>().FirstOrDefault();

        ChangeCurrentScreen(screen);
    }

    protected void ChangeCurrentScreen(Screen sender)
    {
        if (currentScreen != null)
            currentScreen.Close();

        sender.Open();
        currentScreen = sender;
    }

    public void GameScreen()
    {
        OpenScreen<GameScreen>();
        GameController.Instance.Play();
    }
}
