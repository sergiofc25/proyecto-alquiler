using Model.DTO;

public class UserStateService
{
    public DTO_Usuario_Obten_x_Correo CurrentUser { get; private set; }
    public event Action OnChange;

    public void SetUser(DTO_Usuario_Obten_x_Correo user)
    {
        CurrentUser = user;
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}
