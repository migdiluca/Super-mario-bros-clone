using UnityEngine;

namespace SuperMarioBros.Camera
{
    public class CameraController : MonoBehaviour
    {
        public Color defaultColor, darkColor;
        public GameObject player;

        private Transform _transform;
        private UnityEngine.Camera _camera;
        
        // Start is called before the first frame update
        void Start()
        {
            _transform = transform;
            _camera = GetComponent<UnityEngine.Camera>();
        }

        // Update is called once per frame
        void Update()
        {
            Vector2 playerPos = this.player.transform.position,
                cameraPos = this.transform.position;

            if (playerPos.x > cameraPos.x)
                _transform.position = new Vector2(playerPos.x, cameraPos.y);

        }

        public void Center()
        {
            Vector2 playerPos = this.player.transform.position,
                cameraPos = this.transform.position;
            
            _transform.position = new Vector2(playerPos.x, cameraPos.y);
        }

        public void SetDefaultSkyblockColor()
        {
            _camera.backgroundColor = defaultColor;
        }
        
        public void SetDarkSkyblockColor()
        {
            _camera.backgroundColor = darkColor;
        }

        public void SwapSkyblockColor()
        {
            if (_camera.backgroundColor == defaultColor)
                _camera.backgroundColor = darkColor;
            else
                _camera.backgroundColor = defaultColor;
        }
    }
}
