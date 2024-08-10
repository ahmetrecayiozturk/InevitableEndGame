using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Play_Button_To_Game: MonoBehaviour
{
    // Bu metod, butona týklandýðýnda otomatik olarak çaðrýlýr.
    public void OnButtonClick()
    {
        // Sabit sahneye geçiþ yapýlýr.
        SceneManager.LoadScene("Level_1");
    }
}
