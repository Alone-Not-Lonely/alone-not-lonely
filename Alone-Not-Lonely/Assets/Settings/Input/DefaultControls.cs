// GENERATED AUTOMATICALLY FROM 'Assets/Settings/Input/DefaultControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @DefaultControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @DefaultControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""DefaultControls"",
    ""maps"": [
        {
            ""name"": ""Platforming"",
            ""id"": ""14b64a67-cfbf-4113-be54-77da575b4ef1"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""133322ce-494d-4b30-a60a-830a4ee4fbb8"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""9e4f973b-2f13-496e-bc1b-c6ce0ef67205"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Camera"",
                    ""type"": ""Value"",
                    ""id"": ""e11d468c-2bbc-400f-8431-0f52cd2718c7"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""92e30b5d-927f-49f7-818f-893edb07e096"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Use"",
                    ""type"": ""Button"",
                    ""id"": ""23a1d8a2-6c60-4a7b-a4fc-11e8e606d15a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""73248d33-6f36-4bc4-8f6c-4a19596aaae6"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""DefaultControlScheme"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""772480e4-0988-422a-ba36-ea0a94d645ac"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""d2cefc51-bd6c-4c3d-bbbe-1d042bde9475"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""DefaultControlScheme"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""02f10d48-44f5-4f3a-89ee-5958bde2e3bd"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""DefaultControlScheme"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""36250240-3dad-4654-942f-05c177e91350"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""DefaultControlScheme"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""a0a931df-d436-42be-aea1-21fda7ae2566"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""DefaultControlScheme"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""a1d701a1-486a-4439-a561-1d8943a086c2"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""DefaultControlScheme"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""da475291-fcb7-4ee5-9040-a0bf123afffb"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""DefaultControlScheme"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4817f14f-e4f5-4cbc-a635-269729816772"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""DefaultControlScheme"",
                    ""action"": ""Camera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""80646528-4bc6-4eaf-a6f0-737620e1b440"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""DefaultControlScheme"",
                    ""action"": ""Camera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9038aebf-e45d-472b-9e88-dd9ee51228a3"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""DefaultControlScheme"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1c6f4a6d-e792-41c0-a9e1-d94e2369310f"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""DefaultControlScheme"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""99362815-f727-4074-9408-e7f0010b6339"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Use"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""DefaultControlScheme"",
            ""bindingGroup"": ""DefaultControlScheme"",
            ""devices"": []
        }
    ]
}");
        // Platforming
        m_Platforming = asset.FindActionMap("Platforming", throwIfNotFound: true);
        m_Platforming_Move = m_Platforming.FindAction("Move", throwIfNotFound: true);
        m_Platforming_Jump = m_Platforming.FindAction("Jump", throwIfNotFound: true);
        m_Platforming_Camera = m_Platforming.FindAction("Camera", throwIfNotFound: true);
        m_Platforming_Pause = m_Platforming.FindAction("Pause", throwIfNotFound: true);
        m_Platforming_Use = m_Platforming.FindAction("Use", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Platforming
    private readonly InputActionMap m_Platforming;
    private IPlatformingActions m_PlatformingActionsCallbackInterface;
    private readonly InputAction m_Platforming_Move;
    private readonly InputAction m_Platforming_Jump;
    private readonly InputAction m_Platforming_Camera;
    private readonly InputAction m_Platforming_Pause;
    private readonly InputAction m_Platforming_Use;
    public struct PlatformingActions
    {
        private @DefaultControls m_Wrapper;
        public PlatformingActions(@DefaultControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Platforming_Move;
        public InputAction @Jump => m_Wrapper.m_Platforming_Jump;
        public InputAction @Camera => m_Wrapper.m_Platforming_Camera;
        public InputAction @Pause => m_Wrapper.m_Platforming_Pause;
        public InputAction @Use => m_Wrapper.m_Platforming_Use;
        public InputActionMap Get() { return m_Wrapper.m_Platforming; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlatformingActions set) { return set.Get(); }
        public void SetCallbacks(IPlatformingActions instance)
        {
            if (m_Wrapper.m_PlatformingActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnMove;
                @Jump.started -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnJump;
                @Camera.started -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnCamera;
                @Camera.performed -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnCamera;
                @Camera.canceled -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnCamera;
                @Pause.started -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnPause;
                @Use.started -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnUse;
                @Use.performed -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnUse;
                @Use.canceled -= m_Wrapper.m_PlatformingActionsCallbackInterface.OnUse;
            }
            m_Wrapper.m_PlatformingActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Camera.started += instance.OnCamera;
                @Camera.performed += instance.OnCamera;
                @Camera.canceled += instance.OnCamera;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @Use.started += instance.OnUse;
                @Use.performed += instance.OnUse;
                @Use.canceled += instance.OnUse;
            }
        }
    }
    public PlatformingActions @Platforming => new PlatformingActions(this);
    private int m_DefaultControlSchemeSchemeIndex = -1;
    public InputControlScheme DefaultControlSchemeScheme
    {
        get
        {
            if (m_DefaultControlSchemeSchemeIndex == -1) m_DefaultControlSchemeSchemeIndex = asset.FindControlSchemeIndex("DefaultControlScheme");
            return asset.controlSchemes[m_DefaultControlSchemeSchemeIndex];
        }
    }
    public interface IPlatformingActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnCamera(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnUse(InputAction.CallbackContext context);
    }
}
