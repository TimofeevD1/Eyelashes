import ServiceItem from './../serviceItem/ServiceItem'
import './style.css'

const ServicesSection = ({ scrollToOrderNow }) => {
	const services = [
		{
			title: 'Ламинирование ресниц + botox',
			mainImage:
				'https://statimg1.cdnbb8.com/files/image/deal/128715/bb4_deal_big/88905cab82caec35fcbd5bf69db3ad9e.webp?9b7ed2d98a2e34e6fbc45398b1b59a44',
			price: '450',
			newPrice: '450',
			oldPrice: null,
			galleryId: 'gallery-botoxn,',
			images: [
				'https://1.downloader.disk.yandex.ru/preview/8c30362474c999c75dca7935ff902f40a4f3c7c28be138b8ebbc78ce4aa2734b/inf/4sXAJ9Z7__FXkfxFL-Yue5b-OQcsEOw0FDtGhKFznaa9m62JkjrKSQwPb9ZQO-bcqfLC31YAy_olqaPkyVNhhg%3D%3D?uid=1141943229&filename=2025-01-16%2000-21-57.JPG&disposition=inline&hash=&limit=0&content_type=image%2Fjpeg&owner_uid=1141943229&tknv=v2&size=1920x932',
				'https://1.downloader.disk.yandex.ru/preview/8c30362474c999c75dca7935ff902f40a4f3c7c28be138b8ebbc78ce4aa2734b/inf/4sXAJ9Z7__FXkfxFL-Yue5b-OQcsEOw0FDtGhKFznaa9m62JkjrKSQwPb9ZQO-bcqfLC31YAy_olqaPkyVNhhg%3D%3D?uid=1141943229&filename=2025-01-16%2000-21-57.JPG&disposition=inline&hash=&limit=0&content_type=image%2Fjpeg&owner_uid=1141943229&tknv=v2&size=1920x932',
				'https://1.downloader.disk.yandex.ru/preview/8c30362474c999c75dca7935ff902f40a4f3c7c28be138b8ebbc78ce4aa2734b/inf/4sXAJ9Z7__FXkfxFL-Yue5b-OQcsEOw0FDtGhKFznaa9m62JkjrKSQwPb9ZQO-bcqfLC31YAy_olqaPkyVNhhg%3D%3D?uid=1141943229&filename=2025-01-16%2000-21-57.JPG&disposition=inline&hash=&limit=0&content_type=image%2Fjpeg&owner_uid=1141943229&tknv=v2&size=1920x932',
			],
		},
		{
			title: 'Классика',
			mainImage:
				'https://avatars.mds.yandex.net/get-altay/5503221/2a0000017ee9d19c14dca60170daa70982bd/XXL_height',
			price: '500',
			newPrice: '400',
			oldPrice: '500',
			galleryId: 'gallery-1',
			images: [
				'https://avatars.mds.yandex.net/get-altay/5503221/2a0000017ee9d19c14dca60170daa70982bd/XXL_height',
				'https://statimg1.cdnbb8.com/files/image/deal/128715/bb4_deal_big/88905cab82caec35fcbd5bf69db3ad9e.webp?9b7ed2d98a2e34e6fbc45398b1b59a44',
				'https://avatars.mds.yandex.net/get-altay/5503221/2a0000017ee9d19c14dca60170daa70982bd/XXL_height',
				'https://statimg1.cdnbb8.com/files/image/deal/128715/bb4_deal_big/88905cab82caec35fcbd5bf69db3ad9e.webp?9b7ed2d98a2e34e6fbc45398b1b59a44',
				'https://avatars.mds.yandex.net/get-altay/5503221/2a0000017ee9d19c14dca60170daa70982bd/XXL_height',
				'https://statimg1.cdnbb8.com/files/image/deal/128715/bb4_deal_big/88905cab82caec35fcbd5bf69db3ad9e.webp?9b7ed2d98a2e34e6fbc45398b1b59a44',
				'https://avatars.mds.yandex.net/get-altay/5503221/2a0000017ee9d19c14dca60170daa70982bd/XXL_height',
				'https://statimg1.cdnbb8.com/files/image/deal/128715/bb4_deal_big/88905cab82caec35fcbd5bf69db3ad9e.webp?9b7ed2d98a2e34e6fbc45398b1b59a44',
				'https://avatars.mds.yandex.net/get-altay/5503221/2a0000017ee9d19c14dca60170daa70982bd/XXL_height',
				'https://statimg1.cdnbb8.com/files/image/deal/128715/bb4_deal_big/88905cab82caec35fcbd5bf69db3ad9e.webp?9b7ed2d98a2e34e6fbc45398b1b59a44',
				'https://avatars.mds.yandex.net/get-altay/5503221/2a0000017ee9d19c14dca60170daa70982bd/XXL_height',
				'https://statimg1.cdnbb8.com/files/image/deal/128715/bb4_deal_big/88905cab82caec35fcbd5bf69db3ad9e.webp?9b7ed2d98a2e34e6fbc45398b1b59a44',
				'https://avatars.mds.yandex.net/get-altay/5503221/2a0000017ee9d19c14dca60170daa70982bd/XXL_height',
				'https://statimg1.cdnbb8.com/files/image/deal/128715/bb4_deal_big/88905cab82caec35fcbd5bf69db3ad9e.webp?9b7ed2d98a2e34e6fbc45398b1b59a44',
				'https://avatars.mds.yandex.net/get-altay/5503221/2a0000017ee9d19c14dca60170daa70982bd/XXL_height',
				'https://statimg1.cdnbb8.com/files/image/deal/128715/bb4_deal_big/88905cab82caec35fcbd5bf69db3ad9e.webp?9b7ed2d98a2e34e6fbc45398b1b59a44',
				'https://avatars.mds.yandex.net/get-altay/5503221/2a0000017ee9d19c14dca60170daa70982bd/XXL_height',
				'https://statimg1.cdnbb8.com/files/image/deal/128715/bb4_deal_big/88905cab82caec35fcbd5bf69db3ad9e.webp?9b7ed2d98a2e34e6fbc45398b1b59a44',
			],
		},
		{
			title: '2-D Объем',
			mainImage:
				'https://statimg1.cdnbb8.com/files/image/deal/128715/bb4_deal_big/88905cab82caec35fcbd5bf69db3ad9e.webp?9b7ed2d98a2e34e6fbc45398b1b59a44',
			price: '500',
			newPrice: '400',
			oldPrice: '500',
			galleryId: 'gallery-2',
			images: [],
		},
		{
			title: '3-D Объем',
			mainImage:
				'https://statimg1.cdnbb8.com/files/image/deal/128715/bb4_deal_big/88905cab82caec35fcbd5bf69db3ad9e.webp?9b7ed2d98a2e34e6fbc45398b1b59a44',
			price: '450',
			newPrice: '450',
			oldPrice: null,
			galleryId: 'gallery-3',
			images: [],
		},
		// Добавьте другие элементы сервиса...
	]

	return (
		<div className='container'>
			<div className='subtitle text-3xl font-semibold text-gray-700 mb-6 text-center'>
				Профессионально делаю
			</div>
			<div className='services-section grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-4'>
				{services.map((service, index) => (
					<ServiceItem
						key={index}
						{...service}
						scrollToOrderNow={scrollToOrderNow}
						index={index} // передаем индекс для использования в задержке анимации
					/>
				))}
			</div>
		</div>
	)
}

export default ServicesSection
