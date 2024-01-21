using System.Collections;
using System.Collections.Generic;
using Navigation;
using UI.Screens;
using UnityEngine;

namespace UI.Storyline
{
    public class SlideshowManager : MonoBehaviour
    {
        private int _currentSlideshowIndex = 0;

        public static SlideshowManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public void StartSlideshow(List<SlideshowScreen> slideshowScreens)
        {
            StartCoroutine(PlaySlideshow(slideshowScreens));
        }

        private IEnumerator PlaySlideshow(List<SlideshowScreen> slideshowScreens)
        {
            while (_currentSlideshowIndex < slideshowScreens.Count)
            {
                SlideshowScreen currentSlideshowScreens = Instantiate(slideshowScreens[_currentSlideshowIndex],
                    NavigationController.Instance.Canvas.transform);
                yield return new WaitUntil(() => currentSlideshowScreens.IsSlideshowFinished);
                Destroy(currentSlideshowScreens.gameObject);
                _currentSlideshowIndex++;
            }

            _currentSlideshowIndex = 0;
            NavigationController.Instance.ScreenTransition<MainScreen>();
            Debug.Log("All slideshows finished!");
        }
    }
}