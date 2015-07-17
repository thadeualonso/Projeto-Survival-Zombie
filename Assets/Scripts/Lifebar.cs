using UnityEngine;
using UnityEngine.UI;
public class Lifebar : MonoBehaviour {

    public Slider slider;                       // Referência ao slider
    public int MaxHealth;                       // Saúde máxima
    public Image Fill;                          // Atribuir o Fill pelo editor
    public Color MaxHealthColor = Color.green;  // Cor Verde para vida máxima
    public Color MinHealthColor = Color.red;    // Cor Vermelha para vida mínima

    private void Start()
    {
        slider.maxValue = MaxHealth;    // Valor de saúde máximo
        slider.value = MaxHealth;       // Valor de saúde inicial
    }

    // Evento chamado ao manter colisão com algum gameObject
    void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.gameObject.name == "Spike") // Se o nome do objeto for "Spike"
        {
            // Valor do slider subtrai 0.2
            slider.value -= 0.2f;   
            // Função para intercalar entre duas cores em um determinado período de tempo
            Fill.color = Color.Lerp(MinHealthColor, MaxHealthColor, (float) slider.value / MaxHealth);  
        }
    }

}