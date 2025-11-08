using UnityEngine;

// Certifique-se de que a classe DialogueListener não está em um namespace, 
// a menos que você também use esse namespace no NPC.cs
public class DialogueListener : MonoBehaviour
{
    // Este método é chamado automaticamente quando o script é ativado
    void OnEnable()
    {
        // Assina o evento disparado pelo NPC
        // Sempre que OnDialogueEnded for invocado, HandleDialogueEnd será chamado.
        NPC.OnDialogueEnded += HandleDialogueEnd;
    }

    // Este método é chamado automaticamente quando o script é desativado ou destruído
    void OnDisable()
    {
        // Desassina o evento para liberar a referência e evitar erros
        NPC.OnDialogueEnded -= HandleDialogueEnd;
    }

    // O método que é executado em resposta ao evento
    private void HandleDialogueEnd()
    {
        Debug.Log(">>> OUVIDOR ATIVO: Diálogo Encerrado pelo NPC. <<<");
        
        // --- COLOQUE AQUI O CÓDIGO DA AÇÃO QUE DEVE ACONTECER DEPOIS DO DIÁLOGO ---
        
        // Exemplo: Mostrar um botão de missão
        // Exemplo: Desativar um painel de UI específico
        // Exemplo: Liberar o movimento do Player (se o PauseController não fizer isso)
        
        // Se você estivesse em um script de UI, faria algo assim:
        // dialogueUIPanel.SetActive(false);
    }
}