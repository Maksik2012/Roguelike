using System;
using Godot;

namespace Roguelike.Scripts.Utilities
{
    public static class ImageLoader
    {
        private static Image _Image = new Image();
        private static ImageTexture _ImageTexture = new ImageTexture();

        public static ImageTexture LoadTexture(string path, bool isPixelTexture)
        {
            _Image.Load(path);

            if (isPixelTexture)
            {
                _ImageTexture.CreateFromImage(_Image, 0);
            }
            else
            {
                _ImageTexture.CreateFromImage(_Image);
            }

            return _ImageTexture;
        }
    }
}
