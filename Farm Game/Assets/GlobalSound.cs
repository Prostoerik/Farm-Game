using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalSound : MonoBehaviour
{
    public Slider soundSlider; // Ссылка на слайдер в панели Inspector

    public void OnSoundSliderValueChanged()
    {
        float volume = soundSlider.value; // Получаем текущее значение слайдера
        // Применяем новое значение громкости ко всем звукам в игре
        AudioListener.volume = volume;
    }
}
