using System;
using System.Numerics;


namespace Matt_Manleys_Plumbing_Extravaganza.Game.Casting
{
    
    /// <summary>
    /// A participant in the game.
    /// </summary>
    public class Actor
    {

        private bool _enabled = true;
        private Vector2 _position = Vector2.Zero;
        private float _rotation = 0f;
        private float _scale = 1f;
        private Vector2 _size = Vector2.Zero;
        private Color _tint = Color.White();
        private Vector2 _velocity = Vector2.Zero;
        public bool canJump = false;
        

        public Actor() { }


        public virtual void ClampTo(Actor region)
        {
            Validator.CheckNotNull(region);
            
            if (_enabled)
            {
                float x = GetLeft();
                float y = GetTop();

                float maxX = region.GetRight() - GetWidth();
                float maxY = region.GetBottom() - GetHeight();
                float minX = region.GetLeft();
                float minY = region.GetTop();

                x = Math.Clamp(x, minX, maxX);
                y = Math.Clamp(y, minY, maxY);

                Vector2 newPosition = new Vector2(x, y);
                MoveTo(newPosition);
            }
        }


        public int DetectCollisionDirection(Actor other)
        {
            int collisionDirection = 0;
            
            if (this.GetRight() - 5 > other.GetLeft() && 
            this.GetLeft() + 5 < other.GetRight() && 
            this.GetTop() < other.GetBottom())
            {
                // Player collides with bottom of other
                collisionDirection = 1;
            }
            else if (this.GetRight() > other.GetLeft() && 
            this.GetLeft() < other.GetRight() - this.GetWidth() / 2 && 
            (this.GetBottom() > other.GetTop() || this.GetTop() < other.GetBottom()))
            {
                collisionDirection = 2;
            }
            else if (this.GetLeft() < other.GetRight() && 
            this.GetRight() > other.GetLeft() + this.GetWidth() / 2 && 
            (this.GetBottom() > other.GetTop() || this.GetTop() < other.GetBottom()))
            {
                // Player collides with right of other
                collisionDirection = 3;
            }
            if (this.GetRight() - 5 > other.GetLeft() && 
            this.GetLeft() + 5 < other.GetRight() && 
            this.GetBottom() >= other.GetTop() && 
            this.GetTop() < other.GetTop())
            {
                // Player collides with top of other
                collisionDirection = 4;
            }

            return collisionDirection;
        }

        public void DetermineIfCanJump(int collisionDirection)
        {
            if (collisionDirection == 4)
            {
                canJump = true;
            }
            else if (collisionDirection == 2 || collisionDirection == 3)
            {
                canJump = true;
            }
            else
            {
                canJump = false;
            }
        }


        public virtual float GetBottom()
        {
            return _position.Y + _size.Y;
        }


        public virtual Vector2 GetCenter()
        {
            float x = _position.X + (_size.X / 2);
            float y = _position.Y + (_size.Y / 2);
            return new Vector2(x, y);
        }


        public virtual float GetHeight()
        {
            return _size.Y;
        }


        public virtual float GetLeft()
        {
            return _position.X;
        }


        public virtual Vector2 GetPosition()
        {
            return _position;
        }


        public virtual Vector2 GetOriginalSize()
        {
            return _size;
        }


        public virtual float GetRight()
        {
            return _position.X + _size.X;
        }


        public virtual float GetRotation()
        {
            return _rotation;
        }


        public virtual Vector2 GetSize()
        {
            return _size * _scale;
        }


        public virtual Color GetTint()
        {
            return _tint;
        }


        public virtual float GetTop()
        {
            return _position.Y;
        }


        public virtual Vector2 GetVelocity()
        {
            return _velocity;
        }


        public virtual float GetWidth()
        {
            return _size.X;
        }


        public virtual void Move()
        {
            if (_enabled)
            {
                _position = _position + _velocity;
            }
        }


        public virtual void Move(float gravity)
        {
            if (_enabled)
            {
                Vector2 force = new Vector2(_velocity.X, _velocity.Y + gravity);
                _position = _position + force;
            }
        }


        public virtual void MoveTo(Vector2 position)
        {
            _position = position;
        }


        public virtual void MoveTo(float x, float y)
        {
            _position = new Vector2(x, y);
        }


        public virtual bool Overlaps(Actor other)
        {
            return (this.GetLeft() < other.GetRight() && this.GetRight() > other.GetLeft()
                && this.GetTop() < other.GetBottom() && this.GetBottom() > other.GetTop());
        }


        public virtual void SizeTo(Vector2 size) 
        {
            _size = size;
        }


        public virtual void SizeTo(float width, float height) 
        {
            _size = new Vector2(width, height);
        }


        public virtual void Steer(float vx, float vy)
        {
            _velocity = new Vector2(vx, vy);
        }


        public virtual void Tint(Color color)
        {
            Validator.CheckNotNull(color);
            _tint = color;
        }

    }
}