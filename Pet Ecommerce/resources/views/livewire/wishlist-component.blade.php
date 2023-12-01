<main id="main" class="main-site left-sidebar">
    <style>
        .noUi-connect {
    background: #ff2832;
    }
    .product-wish{
        position: absolute;
        top: 10%;
        left: 0;
        z-index: 99;
        right:30px;
        text-align:right;
        padding-top: 0;
    }
    .product-wish .fa{
        color: #cbcbcb'
        font-size:32px;
    }
    .product-wish .fa:hover{
        color: orange;
    }
    .fill-tags{
        color: orange;
    }
    </style>

    <div class="container">

        <div class="wrap-breadcrumb">
            <ul>
                <li class="item-link"><a href="/" class="link">home</a></li>
                <li class="item-link"><span>Wishlist</span></li>
            </ul>
        </div>
        <div class="row">
            @if(Cart::instance('wishlist')->content()->count() > 0)
            <ul class="product-list grid-products equal-container">
                @foreach (Cart::instance('wishlist')->content() as $item)                    
                    <li class="col-lg-3 col-md-6 col-sm-6 col-xs-6 ">
                        <div class="product product-style-3 equal-elem ">
                            <div class="product-thumnail">
                                <a href="{{route('product.details',['slug'=>$item->model->slug])}}" title="{{$item->model->name}}">
                                    <figure><img src="{{ asset('assets/images/products/') }}/{{$item->model->image}}" alt="{{$item->model->name}}"></figure>
                                </a>
                            </div>
                            <div class="item->model->info">
                                <a href="{{route('product.details',['slug'=>$item->model->slug])}}" class="item->model-name"><span>{{$item->model->name}}</span></a>
                                <div class="wrap-price"><span class="item->model-price">{{$item->model->regular_price}}</span></div>
                                <a href="#" class="btn add-to-cart" wire:click.prevent="moveProductFromWishlistToCart('{{$item->rowId}}')">Move To Cart</a>
                                <div class="item->model-wish">                                    
                                    <a href="#" class="btn add-to-cart" wire:click.prevent="removeFromWishlist({{$item->model->id}})">Remove from wish list</a>
                                </div>
                            </div>
                        </div>
                    </li>
                @endforeach                       
            </ul>
            @else
                <h4>Wish List is Empty</h4>
            @endif
        </div>
    </div>
</main>