import { useEffect, useState } from 'react'
import './App.css'

type TaskReadDto = {
  id: number
  title: string
  description: string | null
  done: boolean
  createdAt: string
}

const api = import.meta.env.VITE_API_URL ?? 'http://localhost:5050'

function App() {
  const [items, setItems] = useState<TaskReadDto[]>([])
  const [title, setTitle] = useState('')
  const [description, setDescription] = useState('')
  const [err, setErr] = useState<string | null>(null)

  async function load() {
    setErr(null)
    try {
      const r = await fetch(`${api}/api/Tasks`)
      if (!r.ok) {
        setErr('nie udało się wczytać')
        return
      }
      const data = await r.json()
      setItems(data)
    } catch {
      setErr('brak połączenia z API — sprawdź docker compose i VITE_API_URL')
    }
  }

  useEffect(() => {
    load()
  }, [])

  async function add(e: React.FormEvent) {
    e.preventDefault()
    setErr(null)
    try {
      const r = await fetch(`${api}/api/Tasks`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ title, description: description || null }),
      })
      if (!r.ok) {
        setErr('błąd zapisu')
        return
      }
      setTitle('')
      setDescription('')
      await load()
    } catch {
      setErr('brak połączenia z API — sprawdź docker compose i VITE_API_URL')
    }
  }

  return (
    <div className="wrap">
      <h1>Zadania</h1>
      <form onSubmit={add} className="form">
        <input
          value={title}
          onChange={(e) => setTitle(e.target.value)}
          placeholder="tytuł"
          required
        />
        <input
          value={description}
          onChange={(e) => setDescription(e.target.value)}
          placeholder="opis"
        />
        <button type="submit">Dodaj</button>
      </form>
      {err && <p className="err">{err}</p>}
      <ul className="list">
        {items.map((t) => (
          <li key={t.id}>
            <strong>{t.title}</strong>
            {t.description && <span> — {t.description}</span>}
          </li>
        ))}
      </ul>
    </div>
  )
}

export default App
