
using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Purchasing;

public class InAppProcess : MonoBehaviour, IStoreListener
{
    internal static InAppProcess Instance { get; private set; }

    // Unity IAP objects
    private IStoreController m_Controller;
    private IAppleExtensions m_AppleExtensions;
    private string RemoveAds_ID = "remove_ads";
   
    void Awake()
    {
        Instance = this;
        
        var module = StandardPurchasingModule.Instance();
        // The FakeStore supports: no-ui (always succeeding), basic ui (purchase pass/fail), and
        // developer ui (initialization, purchase, failure code setting). These correspond to
        // the FakeStoreUIMode Enum values passed into StandardPurchasingModule.useFakeStoreUIMode.
        //module.useFakeStoreUIMode = FakeStoreUIMode.StandardUser; //Dùng test không liên quan đến Store

        var builder = ConfigurationBuilder.Instance(module);

        // UnityChannel supports receipt validation through a backend fetch.
        //builder.Configure<IUnityChannelConfiguration>().fetchReceiptPayloadOnPurchase = true;

        //register id san pham
        builder.AddProduct(RemoveAds_ID, ProductType.NonConsumable);

        UnityPurchasing.Initialize(this, builder);
    }

    #region IStoreListener

    /// <summary>
    /// Khởi tạo mua hàng thành công
    /// </summary>
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.LogWarning("==> Khởi tạo mua hàng thành công");
        m_Controller = controller;
        m_AppleExtensions = extensions.GetExtension<IAppleExtensions>();
        // On Apple platforms we need to handle deferred purchases caused by Apple's Ask to Buy feature.
        // On non-Apple platforms this will have no effect; OnDeferred will never be called.
        m_AppleExtensions.RegisterPurchaseDeferredListener(OnDeferred);
    }


    /// <summary>
    /// Khởi tạo IAP failure
    /// </summary>
    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("==> Khởi tạo mua hàng thất bại");
        switch (error)
        {
            case InitializationFailureReason.AppNotKnown:
                Debug.LogError("==> Is your App correctly uploaded on the relevant publisher console?");
                break;
            case InitializationFailureReason.PurchasingUnavailable:
                // Ask the user if billing is disabled in device settings.
                Debug.Log("==> Billing disabled!");
                break;
            case InitializationFailureReason.NoProductsAvailable:
                // Developer configuration error; check product metadata.
                Debug.Log("==> No products available for purchase!");
                break;
        }
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Kết quả mua thành công
    /// </summary>
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
    {
        Debug.Log("++ ==> Purchase OK: " + e.purchasedProduct.definition.id);
        Debug.Log("++ ==> Receipt: " + e.purchasedProduct.receipt);
//        SetThongBao(e.purchasedProduct.receipt);

        SuccesBuyIap();
        Debug.LogWarning("=====> MUA THÀNH CÔNG");
        return PurchaseProcessingResult.Complete;
    }

    /// <summary>
    /// Mua vật phẩm không thành công
    /// </summary>
    public void OnPurchaseFailed(Product i, PurchaseFailureReason p)
    {
        Debug.LogWarning("==> Mua hàng thất bại");
        Debug.Log("==> Purchase failed: " + i.definition.id);
    }

    /// <summary>
    /// iOS Specific.
    /// This is called as part of Apple's 'Ask to buy' functionality,
    /// when a purchase is requested by a minor and referred to a parent
    /// for approval.
    ///
    /// When the purchase is approved or rejected, the normal purchase events will fire.
    /// </summary>
    /// <param name="item">Item.</param>
    private void OnDeferred(Product item)
    {
        Debug.Log("==> Purchase deferred: " + item.definition.id);
    }

    #endregion

    /// <summary>
    /// Kiểm tra đã khởi tạo mua hàng chưa
    /// </summary>
    /// <returns></returns>
    private bool IsInitialized()
    {
        // Only say we are initialized if both the Purchasing references are set.
        return m_Controller != null && m_AppleExtensions != null;
    }

    /// <summary>
    /// Mua vật phẩm
    /// </summary>
    /// <param name="productId">id sản phẩm cần mua</param>
    public void InitBuyIAP()
    {
        // If Purchasing has been initialized ...
        if (IsInitialized())
        {
            // Tìm kiếm sản phẩm trong kho của thiết bị này
            Product product = m_Controller.products.WithID(RemoveAds_ID);

            // Nếu có sản phẩm trong kho thiết bị này và nó sẵn sàng để bán
            if (product != null && product.availableToPurchase)
            {
                //Debug.LogWarning(string.Format("\n\n\n==> Gui yêu cầu mua sản phẩm với ID: '{0}'", product.definition.id));
                // Gửi yêu cầu mua đợi kết quả trả về qua 2 hàm: ProcessPurchase() or OnPurchaseFailed() asynchronously.
                m_Controller.InitiatePurchase(product);
            }
            // Otherwise ...
            else
            {
                // ... report the product look-up failure situation  
                Debug.Log(
                    "==> BuyProductID: FAIL. Không mua sản phẩm hoặc không tìm thấy sản phẩm để mua: ");
            }
        }
        // Otherwise ...
        else
        {
           
        }
    }
    public void SuccesBuyIap(){
        PlayerPrefs.SetInt("NO_ADS", 1);
        Admob.Instance.CheckNoAds();
    }
  
}
