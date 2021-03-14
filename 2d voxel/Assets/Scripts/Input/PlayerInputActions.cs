// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Input System/PlayerInputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInputActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""standard"",
            ""id"": ""be71e5ca-1bae-4b8b-b6c8-b0718b5e0f86"",
            ""actions"": [
                {
                    ""name"": ""Run"",
                    ""type"": ""Button"",
                    ""id"": ""cd6118d9-14f6-4857-b013-7a8fc03160bb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""22ab1ef9-1ec9-40cc-aaaa-cfd278851d0b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Trigger1"",
                    ""type"": ""Button"",
                    ""id"": ""0d161803-3629-4b58-8471-3a5567719c44"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Trigger2"",
                    ""type"": ""Button"",
                    ""id"": ""974b303a-f6c2-462e-947b-95a3031df985"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseVelocity"",
                    ""type"": ""Value"",
                    ""id"": ""e215967f-9178-44f3-86bf-6828b4499117"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ToggleInventory"",
                    ""type"": ""Button"",
                    ""id"": ""c5484d14-7d3e-44cd-b87b-47d7d42b1be8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""keyboard"",
                    ""id"": ""a920ea16-8db2-4f36-93bf-4dc9a710c682"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""4be653d8-4ed4-4d61-bfc6-861f22d5344b"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""77b03560-51b8-4cf1-aa2d-1031fd42eac8"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""35c44ba6-d350-4d32-887f-4c8e38fe5b25"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6f8946b3-1a45-4fc0-94c0-16f480d0d030"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Trigger1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ff5def25-bdc4-4444-93a4-5131b33d376f"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Trigger2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""80d9a282-f3cf-4d43-9ccf-1c702a6e051e"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseVelocity"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""58c8a5dc-10a2-42a4-b31b-29e847f73184"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleInventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""debug"",
            ""id"": ""d888c831-3c05-4104-ab88-334c7f83936e"",
            ""actions"": [
                {
                    ""name"": ""ToggleDC"",
                    ""type"": ""Button"",
                    ""id"": ""13de6e74-ef97-4d1b-a407-18f13a7d5ded"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Enter"",
                    ""type"": ""Button"",
                    ""id"": ""9da63e62-62fe-4a7e-8127-2148379e9734"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""AnyKey"",
                    ""type"": ""Button"",
                    ""id"": ""03a54a88-7fa5-426e-8692-e9d716d847da"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""025f160e-eee3-4ed2-8299-9ce17e468ff0"",
                    ""path"": ""<Keyboard>/slash"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleDC"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e5c02e71-0866-4533-a531-e99695f151ae"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Enter"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""30b29de2-445c-41fa-a00f-0978f074bdf8"",
                    ""path"": ""<Keyboard>/anyKey"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AnyKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // standard
        m_standard = asset.FindActionMap("standard", throwIfNotFound: true);
        m_standard_Run = m_standard.FindAction("Run", throwIfNotFound: true);
        m_standard_Jump = m_standard.FindAction("Jump", throwIfNotFound: true);
        m_standard_Trigger1 = m_standard.FindAction("Trigger1", throwIfNotFound: true);
        m_standard_Trigger2 = m_standard.FindAction("Trigger2", throwIfNotFound: true);
        m_standard_MouseVelocity = m_standard.FindAction("MouseVelocity", throwIfNotFound: true);
        m_standard_ToggleInventory = m_standard.FindAction("ToggleInventory", throwIfNotFound: true);
        // debug
        m_debug = asset.FindActionMap("debug", throwIfNotFound: true);
        m_debug_ToggleDC = m_debug.FindAction("ToggleDC", throwIfNotFound: true);
        m_debug_Enter = m_debug.FindAction("Enter", throwIfNotFound: true);
        m_debug_AnyKey = m_debug.FindAction("AnyKey", throwIfNotFound: true);
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

    // standard
    private readonly InputActionMap m_standard;
    private IStandardActions m_StandardActionsCallbackInterface;
    private readonly InputAction m_standard_Run;
    private readonly InputAction m_standard_Jump;
    private readonly InputAction m_standard_Trigger1;
    private readonly InputAction m_standard_Trigger2;
    private readonly InputAction m_standard_MouseVelocity;
    private readonly InputAction m_standard_ToggleInventory;
    public struct StandardActions
    {
        private @PlayerInputActions m_Wrapper;
        public StandardActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Run => m_Wrapper.m_standard_Run;
        public InputAction @Jump => m_Wrapper.m_standard_Jump;
        public InputAction @Trigger1 => m_Wrapper.m_standard_Trigger1;
        public InputAction @Trigger2 => m_Wrapper.m_standard_Trigger2;
        public InputAction @MouseVelocity => m_Wrapper.m_standard_MouseVelocity;
        public InputAction @ToggleInventory => m_Wrapper.m_standard_ToggleInventory;
        public InputActionMap Get() { return m_Wrapper.m_standard; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(StandardActions set) { return set.Get(); }
        public void SetCallbacks(IStandardActions instance)
        {
            if (m_Wrapper.m_StandardActionsCallbackInterface != null)
            {
                @Run.started -= m_Wrapper.m_StandardActionsCallbackInterface.OnRun;
                @Run.performed -= m_Wrapper.m_StandardActionsCallbackInterface.OnRun;
                @Run.canceled -= m_Wrapper.m_StandardActionsCallbackInterface.OnRun;
                @Jump.started -= m_Wrapper.m_StandardActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_StandardActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_StandardActionsCallbackInterface.OnJump;
                @Trigger1.started -= m_Wrapper.m_StandardActionsCallbackInterface.OnTrigger1;
                @Trigger1.performed -= m_Wrapper.m_StandardActionsCallbackInterface.OnTrigger1;
                @Trigger1.canceled -= m_Wrapper.m_StandardActionsCallbackInterface.OnTrigger1;
                @Trigger2.started -= m_Wrapper.m_StandardActionsCallbackInterface.OnTrigger2;
                @Trigger2.performed -= m_Wrapper.m_StandardActionsCallbackInterface.OnTrigger2;
                @Trigger2.canceled -= m_Wrapper.m_StandardActionsCallbackInterface.OnTrigger2;
                @MouseVelocity.started -= m_Wrapper.m_StandardActionsCallbackInterface.OnMouseVelocity;
                @MouseVelocity.performed -= m_Wrapper.m_StandardActionsCallbackInterface.OnMouseVelocity;
                @MouseVelocity.canceled -= m_Wrapper.m_StandardActionsCallbackInterface.OnMouseVelocity;
                @ToggleInventory.started -= m_Wrapper.m_StandardActionsCallbackInterface.OnToggleInventory;
                @ToggleInventory.performed -= m_Wrapper.m_StandardActionsCallbackInterface.OnToggleInventory;
                @ToggleInventory.canceled -= m_Wrapper.m_StandardActionsCallbackInterface.OnToggleInventory;
            }
            m_Wrapper.m_StandardActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Run.started += instance.OnRun;
                @Run.performed += instance.OnRun;
                @Run.canceled += instance.OnRun;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Trigger1.started += instance.OnTrigger1;
                @Trigger1.performed += instance.OnTrigger1;
                @Trigger1.canceled += instance.OnTrigger1;
                @Trigger2.started += instance.OnTrigger2;
                @Trigger2.performed += instance.OnTrigger2;
                @Trigger2.canceled += instance.OnTrigger2;
                @MouseVelocity.started += instance.OnMouseVelocity;
                @MouseVelocity.performed += instance.OnMouseVelocity;
                @MouseVelocity.canceled += instance.OnMouseVelocity;
                @ToggleInventory.started += instance.OnToggleInventory;
                @ToggleInventory.performed += instance.OnToggleInventory;
                @ToggleInventory.canceled += instance.OnToggleInventory;
            }
        }
    }
    public StandardActions @standard => new StandardActions(this);

    // debug
    private readonly InputActionMap m_debug;
    private IDebugActions m_DebugActionsCallbackInterface;
    private readonly InputAction m_debug_ToggleDC;
    private readonly InputAction m_debug_Enter;
    private readonly InputAction m_debug_AnyKey;
    public struct DebugActions
    {
        private @PlayerInputActions m_Wrapper;
        public DebugActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @ToggleDC => m_Wrapper.m_debug_ToggleDC;
        public InputAction @Enter => m_Wrapper.m_debug_Enter;
        public InputAction @AnyKey => m_Wrapper.m_debug_AnyKey;
        public InputActionMap Get() { return m_Wrapper.m_debug; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DebugActions set) { return set.Get(); }
        public void SetCallbacks(IDebugActions instance)
        {
            if (m_Wrapper.m_DebugActionsCallbackInterface != null)
            {
                @ToggleDC.started -= m_Wrapper.m_DebugActionsCallbackInterface.OnToggleDC;
                @ToggleDC.performed -= m_Wrapper.m_DebugActionsCallbackInterface.OnToggleDC;
                @ToggleDC.canceled -= m_Wrapper.m_DebugActionsCallbackInterface.OnToggleDC;
                @Enter.started -= m_Wrapper.m_DebugActionsCallbackInterface.OnEnter;
                @Enter.performed -= m_Wrapper.m_DebugActionsCallbackInterface.OnEnter;
                @Enter.canceled -= m_Wrapper.m_DebugActionsCallbackInterface.OnEnter;
                @AnyKey.started -= m_Wrapper.m_DebugActionsCallbackInterface.OnAnyKey;
                @AnyKey.performed -= m_Wrapper.m_DebugActionsCallbackInterface.OnAnyKey;
                @AnyKey.canceled -= m_Wrapper.m_DebugActionsCallbackInterface.OnAnyKey;
            }
            m_Wrapper.m_DebugActionsCallbackInterface = instance;
            if (instance != null)
            {
                @ToggleDC.started += instance.OnToggleDC;
                @ToggleDC.performed += instance.OnToggleDC;
                @ToggleDC.canceled += instance.OnToggleDC;
                @Enter.started += instance.OnEnter;
                @Enter.performed += instance.OnEnter;
                @Enter.canceled += instance.OnEnter;
                @AnyKey.started += instance.OnAnyKey;
                @AnyKey.performed += instance.OnAnyKey;
                @AnyKey.canceled += instance.OnAnyKey;
            }
        }
    }
    public DebugActions @debug => new DebugActions(this);
    public interface IStandardActions
    {
        void OnRun(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnTrigger1(InputAction.CallbackContext context);
        void OnTrigger2(InputAction.CallbackContext context);
        void OnMouseVelocity(InputAction.CallbackContext context);
        void OnToggleInventory(InputAction.CallbackContext context);
    }
    public interface IDebugActions
    {
        void OnToggleDC(InputAction.CallbackContext context);
        void OnEnter(InputAction.CallbackContext context);
        void OnAnyKey(InputAction.CallbackContext context);
    }
}
