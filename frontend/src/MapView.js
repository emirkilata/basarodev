import { MapContainer, TileLayer, FeatureGroup, Marker, Polyline, Polygon } from 'react-leaflet';
import { EditControl } from 'react-leaflet-draw';
import axios from 'axios';
import 'leaflet/dist/leaflet.css';
import 'leaflet-draw/dist/leaflet.draw.css';
import { useEffect, useState } from 'react';
import L from 'leaflet';
import markerIcon2x from 'leaflet/dist/images/marker-icon-2x.png';
import markerIcon from 'leaflet/dist/images/marker-icon.png';
import markerShadow from 'leaflet/dist/images/marker-shadow.png';
import './MapViewSidebar.css';

delete L.Icon.Default.prototype._getIconUrl;
L.Icon.Default.mergeOptions({
  iconRetinaUrl: markerIcon2x,
  iconUrl: markerIcon,
  shadowUrl: markerShadow,
});

export default function MapView() {
  const [points, setPoints] = useState([]);
  const [lines, setLines] = useState([]);
  const [polygons, setPolygons] = useState([]);
  const [pointsMap, setPointsMap] = useState({});
  const [linesMap, setLinesMap] = useState({});
  const [polygonsMap, setPolygonsMap] = useState({});
  const [sidebarOpen, setSidebarOpen] = useState(false);
  const [activeTab, setActiveTab] = useState('points');

  useEffect(() => {
    // Tüm point, line ve polygonları backend'den çek
    const fetchShapes = async () => {
      try {
        const [pointRes, lineRes, polygonRes] = await Promise.all([
          axios.get('http://localhost:5157/api/point'),
          axios.get('http://localhost:5157/api/line'),
          axios.get('http://localhost:5157/api/polygon'),
        ]);
        setPoints(pointRes.data.data || []);
        setLines(lineRes.data.data || []);
        setPolygons(polygonRes.data.data || []);
        // Map'leri oluştur
        const pMap = {};
        (pointRes.data.data || []).forEach(p => {
          pMap[`${p.x},${p.y}`] = p.id;
        });
        setPointsMap(pMap);
        const lMap = {};
        (lineRes.data.data || []).forEach(l => {
          try {
            const geo = typeof l.geometry === 'string' ? JSON.parse(l.geometry) : l.geometry;
            lMap[JSON.stringify(geo.geometry.coordinates)] = l.id;
          } catch {}
        });
        setLinesMap(lMap);
        const polyMap = {};
        (polygonRes.data.data || []).forEach(poly => {
          try {
            const geo = typeof poly.geometry === 'string' ? JSON.parse(poly.geometry) : poly.geometry;
            polyMap[JSON.stringify(geo.geometry.coordinates)] = poly.id;
          } catch {}
        });
        setPolygonsMap(polyMap);
      } catch (err) {
        console.error('Şekiller yüklenirken hata:', err);
      }
    };
    fetchShapes();
  }, []);

  const onCreated = async (e) => {
    const geojson = e.layer.toGeoJSON();
    console.log('Çizilen şekil:', geojson);

    // Kullanıcıdan isim iste
    const shapeType = geojson.geometry.type;
    let defaultName = '';
    if (shapeType === 'Point') defaultName = 'Nokta';
    else if (shapeType === 'LineString') defaultName = 'Çizgi';
    else if (shapeType === 'Polygon') defaultName = 'Poligon';
    const name = window.prompt(`${defaultName} için isim girin:`, `${defaultName} ${Date.now()}`);
    if (!name) {
      alert('İsim girilmeden şekil kaydedilemez!');
      return;
    }

    try {
      let url = '';
      let data = {
        name: name,
        geoJson: JSON.stringify(geojson)
      };

      // Şekil tipine göre endpoint seç
      if (geojson.geometry.type === 'Point') {
        url = 'http://localhost:5157/api/point';
        data = {
          name: name,
          x: geojson.geometry.coordinates[0],
          y: geojson.geometry.coordinates[1]
        };
      } else if (geojson.geometry.type === 'LineString') {
        url = 'http://localhost:5157/api/line';
      } else if (geojson.geometry.type === 'Polygon') {
        url = 'http://localhost:5157/api/polygon';
      }

      if (url) {
        const response = await axios.post(url, data);
        console.log('Başarıyla kaydedildi:', response.data);
        alert('Şekil başarıyla kaydedildi!');
      }
    } catch (error) {
      console.error('API Hatası:', error);
      alert('Şekil kaydedilirken hata oluştu!');
    }
  };

  const onDeleted = async (e) => {
    // Silinen şekillerin layer'larını al
    const layers = e.layers;
    let deleted = false;
    layers.eachLayer(async (layer) => {
      const geojson = layer.toGeoJSON();
      let id = null;
      let url = '';
      if (geojson.geometry.type === 'Point') {
        id = pointsMap[`${geojson.geometry.coordinates[0]},${geojson.geometry.coordinates[1]}`];
        url = `http://localhost:5157/api/point/${id}`;
      } else if (geojson.geometry.type === 'LineString') {
        id = linesMap[JSON.stringify(geojson.geometry.coordinates)];
        url = `http://localhost:5157/api/line/${id}`;
      } else if (geojson.geometry.type === 'Polygon') {
        id = polygonsMap[JSON.stringify(geojson.geometry.coordinates)];
        url = `http://localhost:5157/api/polygon/${id}`;
      }
      if (url && id) {
        try {
          await axios.delete(url);
          deleted = true;
        } catch (err) {
          console.error('Silme hatası:', err);
        }
      }
    });
    if (deleted) {
      // Silme sonrası şekilleri tekrar çek
      try {
        const [pointRes, lineRes, polygonRes] = await Promise.all([
          axios.get('http://localhost:5157/api/point'),
          axios.get('http://localhost:5157/api/line'),
          axios.get('http://localhost:5157/api/polygon'),
        ]);
        setPoints(pointRes.data.data || []);
        setLines(lineRes.data.data || []);
        setPolygons(polygonRes.data.data || []);
        // Map'leri güncelle
        const pMap = {};
        (pointRes.data.data || []).forEach(p => {
          pMap[`${p.x},${p.y}`] = p.id;
        });
        setPointsMap(pMap);
        const lMap = {};
        (lineRes.data.data || []).forEach(l => {
          try {
            const geo = typeof l.geometry === 'string' ? JSON.parse(l.geometry) : l.geometry;
            lMap[JSON.stringify(geo.geometry.coordinates)] = l.id;
          } catch {}
        });
        setLinesMap(lMap);
        const polyMap = {};
        (polygonRes.data.data || []).forEach(poly => {
          try {
            const geo = typeof poly.geometry === 'string' ? JSON.parse(poly.geometry) : poly.geometry;
            polyMap[JSON.stringify(geo.geometry.coordinates)] = poly.id;
          } catch {}
        });
        setPolygonsMap(polyMap);
      } catch (err) {
        console.error('Şekiller güncellenirken hata:', err);
      }
    }
  };

  return (
    <div style={{ display: 'flex', height: '100vh', width: '100vw' }}>
      {/* Sidebar */}
      <div className={`sidebar${sidebarOpen ? ' open' : ''}`}>
        <button className="sidebar-toggle" onClick={() => setSidebarOpen(!sidebarOpen)}>
          {sidebarOpen ? '←' : '→'}
        </button>
        {sidebarOpen && (
          <div className="sidebar-content">
            <div className="sidebar-tabs">
              <button className={activeTab === 'points' ? 'active' : ''} onClick={() => setActiveTab('points')}>Pointler</button>
              <button className={activeTab === 'lines' ? 'active' : ''} onClick={() => setActiveTab('lines')}>Çizgiler</button>
              <button className={activeTab === 'polygons' ? 'active' : ''} onClick={() => setActiveTab('polygons')}>Poligonlar</button>
            </div>
            <div className="sidebar-list">
              {activeTab === 'points' && (
                <ul>
                  {points.length === 0 && <li>Hiç point yok.</li>}
                  {points.map((p) => (
                    <li key={p.id}>{p.name}</li>
                  ))}
                </ul>
              )}
              {activeTab === 'lines' && (
                <ul>
                  {lines.length === 0 && <li>Hiç çizgi yok.</li>}
                  {lines.map((l) => (
                    <li key={l.id}>{l.name}</li>
                  ))}
                </ul>
              )}
              {activeTab === 'polygons' && (
                <ul>
                  {polygons.length === 0 && <li>Hiç poligon yok.</li>}
                  {polygons.map((poly) => (
                    <li key={poly.id}>{poly.name}</li>
                  ))}
                </ul>
              )}
            </div>
          </div>
        )}
      </div>
      {/* Harita */}
      <div style={{ flex: 1 }}>
        <MapContainer center={[39.92, 32.85]} zoom={6} style={{ height: "100vh", width: "100%" }} zoomControl={false}>
          <TileLayer url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png" />
          <FeatureGroup>
            {/* Kayıtlı pointleri göster */}
            {points.map((p, i) => (
              <Marker key={`point-${p.id}`} position={[p.y, p.x]} ref={ref => { if (ref) ref.options._dbid = p.id; }} />
            ))}
            {/* Kayıtlı line'ları göster */}
            {lines.map((l, i) => {
              let coords = [];
              try {
                const geo = typeof l.geometry === 'string' ? JSON.parse(l.geometry) : l.geometry;
                const coordinates = geo.geometry.coordinates;
                coords = coordinates.map(c => [c[1], c[0]]);
              } catch {}
              return <Polyline key={`line-${l.id}`} positions={coords} ref={ref => { if (ref) ref.options._dbid = l.id; }} />;
            })}
            {/* Kayıtlı polygonları göster */}
            {polygons.map((poly, i) => {
              let coords = [];
              try {
                const geo = typeof poly.geometry === 'string' ? JSON.parse(poly.geometry) : poly.geometry;
                const coordinates = geo.geometry.coordinates;
                coords = coordinates[0].map(c => [c[1], c[0]]);
              } catch {}
              return <Polygon key={`polygon-${poly.id}`} positions={coords} ref={ref => { if (ref) ref.options._dbid = poly.id; }} />;
            })}
            <EditControl
              position="topright"
              onCreated={onCreated}
              onDeleted={onDeleted}
              draw={{
                rectangle: false,
                circle: false,
                circlemarker: false
              }}
            />
          </FeatureGroup>
        </MapContainer>
      </div>
    </div>
  );
} 