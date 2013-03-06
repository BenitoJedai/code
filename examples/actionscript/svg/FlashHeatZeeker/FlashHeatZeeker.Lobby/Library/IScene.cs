using System;
namespace InteractivePromotionB.Components
{
    public interface IScene
    {
        string name { get; set; }
        void PlayScene(Action Done);
        event Action LinkNotification;
        event Action LinkDenotification;
    }
}
