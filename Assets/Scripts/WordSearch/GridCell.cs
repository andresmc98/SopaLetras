using UnityEngine;
using TMPro;

/// <summary>
/// Representa una celda del grid de la sopa de letras.
/// Contiene la letra y maneja su estado visual.
/// </summary>
public class GridCell : MonoBehaviour
{
    [SerializeField] private TextMeshPro _letterText;

    public char Letter { get; private set; }
    public int Row     { get; private set; }
    public int Col     { get; private set; }

    public void Init(char letter, int row, int col)
    {
        Letter = letter;
        Row    = row;
        Col    = col;
        _letterText.text = letter.ToString();
    }

    public void SetHighlight(bool highlighted)
    {
        // TODO: cambiar color según estado de selección
    }

    public void SetFound(bool found)
    {
        // TODO: animación cuando la palabra es encontrada
    }
}
