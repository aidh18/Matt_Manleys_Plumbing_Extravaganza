using System;
using System.Collections.Generic;
using Matt_Manleys_Plumbing_Extravaganza.Game.Casting;
using Matt_Manleys_Plumbing_Extravaganza.Game.Scripting;
using Matt_Manleys_Plumbing_Extravaganza.Game.Services;


namespace Example.Images
{
    /// <summary>
    /// Draws the actors on the screen.
    /// </summary>
    public class DrawImageAction : Matt_Manleys_Plumbing_Extravaganza.Game.Scripting.Action
    {
        private IVideoService _videoService;

        public DrawImageAction(IServiceFactory serviceFactory)
        {
            _videoService = serviceFactory.GetVideoService();
        }

        public override void Execute(Scene scene, float deltaTime, IActionCallback callback)
        {
            try
            {
                // get the actors from the cast
                List<Image> robots = scene.GetAllActors<Image>("robots");

                // draw the actors on the screen using the video service
                _videoService.ClearBuffer();
                _videoService.Draw(robots);
                _videoService.FlushBuffer();
            }
            catch (Exception exception)
            {
                callback.OnError("Couldn't draw robots.", exception);
            }
        }
    }
}