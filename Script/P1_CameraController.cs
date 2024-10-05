public class P1_CameraController : CameraControllerBase
{
    protected override string CameraCubeName => "Player1_CameraCube";
    protected override string FighterName => "Player1_Fighter";
    protected override System.Type ControllerType => typeof(P_Controller_Platform);
}